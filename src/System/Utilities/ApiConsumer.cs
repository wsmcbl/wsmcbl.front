using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.View.Config;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace wsmcbl.src.Utilities;

public class ApiConsumer
{
    private readonly HttpClient httpClient;
    private readonly Notificator service;

    public ApiConsumer(HttpClient httpClient, Notificator notificator)
    {
        this.httpClient = httpClient;
        service = notificator;
        SetServerUri();
    }

    
    private Uri? _server;
    private void SetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if(string.IsNullOrEmpty(api))
        {
            api = "http://localhost:4000";
        }

        _server = new Uri($"{api}/v2");
    }

    private Uri BuildUri(Modules modules, string resource)
    {
        var moduleDir = modules switch
        {
            Modules.Academy => "academy",
            Modules.Secretary => "secretary",
            Modules.Accounting => "accounting",
            Modules.Config => "users",
            _ => ""
        };
        return new Uri($"{_server}/{moduleDir}/{resource.TrimStart('/')}");
    }

    public async Task<T> GetAsync<T>(Modules module, string resource, T defaultResult)
    {
        var response = await httpClient.GetAsync(BuildUri(module, resource));
        return await Template(defaultResult, response);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(Modules modules, string resource, TRequest data,
        TResponse defaultResult)
    {
        var response = await httpClient.PostAsJsonAsync(BuildUri(modules, resource), data);
        return await Template(defaultResult, response);
    }

    public async Task<string> LoginAsync(LoginDto data)
    {
        var defaultDto = new LoginDto();
        defaultDto.setDefault();
        var response = await PostAsync(Modules.Config, "tokens", data, defaultDto);
        return response!.token;
    }

    public async Task<bool> PutAsync<T>(Modules modules, string resource, T data)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(BuildUri(modules, resource), data);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem.Status}) {problem.Detail}");
            }

            return true;
        }
        catch (InternalException ex)
        {
            await service.ShowError(ex.Title, ex.Content);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }
        return false;
    }

    public async Task<byte[]> GetPdfAsync(Modules module, string resource)
    {
        try
        {
            var response = await httpClient.GetAsync(BuildUri(module, resource));
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem.Status}) {problem.Detail}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (InternalException ex)
        {
            await service.ShowError(ex.Title, ex.Content);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }

        return [];
    }


    private async Task<T> Template<T>(T defaultResult, HttpResponseMessage response)
    {
        try
        {
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem.Status}) {problem.Detail}");
            }

            defaultResult = await response.Content.ReadFromJsonAsync<T>();
        }
        catch (InternalException ex)
        {
            await service.ShowError(ex.Title, ex.Content);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", $"{ex.Message} Trace: {ex.StackTrace}");
        }

        return defaultResult;
    }

    public void SetToken(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public void ResetToken()
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
