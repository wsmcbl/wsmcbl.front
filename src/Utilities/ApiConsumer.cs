using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace wsmcbl.src.Utilities;

public class ApiConsumer
{
    private readonly HttpClient httpClient;
    private readonly Notificator service;

    private readonly string _connectionString;

    public ApiConsumer(HttpClient httpClient, Notificator notificator)
    {
        this.httpClient = httpClient;
        service = notificator;
        _connectionString = "http://localhost:4000/v1";
    }

    private Uri BuildUri(Modules modules, string resource)
    {
        var moduleDir = modules switch
        {
            Modules.Academy => "academy",
            Modules.Secretary => "secretary",
            Modules.Accounting => "accounting",
            _ => ""
        };

        return new Uri($"{_connectionString}/{moduleDir}/{resource.TrimStart('/')}");
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

    public async Task<byte[]?> GetPdfAsync(Modules module, string resource, byte[] defaultResult)
    {
        var result = defaultResult;
        try
        {
            var response = await httpClient.GetAsync(BuildUri(module, resource));
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem.Status}) {problem.Detail}");
            }

            result = await response.Content.ReadAsByteArrayAsync();
        }
        catch (InternalException ex)
        {
            await service.ShowError(ex.Title, ex.Content);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }

        return result;
    }


    private async Task<T> Template<T>(T defaultResult, HttpResponseMessage response)
    {
        var result = defaultResult;
        try
        {
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new InternalException("Hubo un problema con la solicitud.",
                    $"({problem.Status}) {problem.Detail}");
            }

            result = await response.Content.ReadFromJsonAsync<T>();
        }
        catch (InternalException ex)
        {
            await service.ShowError(ex.Title, ex.Content);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }

        return result;
    }
}