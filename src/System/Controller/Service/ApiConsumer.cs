using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumer
{
    private readonly HttpClient httpClient;
    private readonly Notificator service;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly Uri _server;

    public ApiConsumer(HttpClient httpClient, Notificator notificator, ProtectedLocalStorage localStorage)
    {
        this.httpClient = httpClient;
        service = notificator;
        _localStorage = localStorage;
        _server = GetServerUri();
    }

    private Uri BuildUri(Modules modules, string resource)
    {
        var moduleDir = modules switch
        {
            Modules.Academy => "academy",
            Modules.Secretary => "secretary",
            Modules.Accounting => "accounting",
            Modules.Config => "config",
            _ => ""
        };
        return new Uri($"{_server}/{moduleDir}/{resource.TrimStart('/')}");
    }

    public async Task<T> GetAsync<T>(Modules module, string resource, T defaultResult)
    {   
        await LoadToken();
        var response = await httpClient.GetAsync(BuildUri(module, resource));
        return (await Template(defaultResult, response))!;
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(Modules modules, string resource, TRequest data,
        TResponse defaultResult)
    {
        await LoadToken();
        var response = await httpClient.PostAsJsonAsync(BuildUri(modules, resource), data);
        return await Template(defaultResult, response);
    }

    public async Task<bool> PutAsync<T>(Modules modules, string resource, T data)
    {
        try
        {
            await LoadToken();
            var response = await httpClient.PutAsJsonAsync(BuildUri(modules, resource), data);
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new InternalException("Hubo un problema con la solicitud.",
                $"({problem!.Status}) {problem.Detail}");
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
    
    public async Task<bool> PutAsyncPhoto(Modules modules, string resource, HttpContent content)
    {
        try
        {
            await LoadToken();
            var requestUri = BuildUri(modules, resource);
            var response = await httpClient.PutAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem?.Status}) {problem?.Detail}");
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
            await LoadToken();
            var response = await httpClient.GetAsync(BuildUri(module, resource));
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            throw new InternalException("Hubo un problema con la solicitud.",
                $"({problem!.Status}) {problem.Detail}");
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

    private async Task<T?> Template<T>(T defaultResult, HttpResponseMessage response)
    {
        try
        {
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem!.Status}) {problem.Detail} {problem.GetValidationErrors()}");
            }

            defaultResult = (await response.Content.ReadFromJsonAsync<T>())!;
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
    
    
    public async Task<string> LoginAsync(LoginDto data)
    {
        var defaultDto = new LoginDto();
        defaultDto.SetAsDefault();
        var response = await PostAsync(Modules.Config, "users/tokens", data, defaultDto);
        return response!.token!;
    }

    private async Task LoadToken()
    {
        var tokenResult = await _localStorage.GetAsync<string>(Utilities.Utilities.TokenKey);
        var token = tokenResult.Value;

        httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token?.Trim()) ? null :
            new AuthenticationHeaderValue("Bearer", token);
    }
    
    
    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api))
            throw new InternalException("API environment variable not found.");
        
        return new Uri($"{api}/v2");
    }
}