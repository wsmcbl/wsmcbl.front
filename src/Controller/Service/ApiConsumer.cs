using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumer
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly Uri _server;

    public ApiConsumer(HttpClient httpClient, ProtectedLocalStorage localStorage)
    {
        _httpClient = httpClient;
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
            Modules.Management => "management",
            _ => ""
        };
        return new Uri($"{_server}/{moduleDir}/{resource.TrimStart('/')}");
    }

    public async Task<T> GetAsync<T>(Modules module, string resource, T defaultResult)
    {
        await LoadToken();
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<T>(), defaultResult, response);
    }
    
    public async Task<byte[]> GetByteFileAsync(Modules module, string resource)
    {
        await LoadToken();
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadAsByteArrayAsync()!, [], response);
    }

    public async Task<R> PostAsync<T, R>(Modules modules, string resource, T? data, R defaultResult)
    {
        await LoadToken();

        StringContent? content = null;
        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        var response = await _httpClient.PostAsync(BuildUri(modules, resource), content);
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<R>(), defaultResult, response);
    }
    
    public async Task<bool> PutAsync<T>(Modules modules, string resource, T? data)
    {
        await LoadToken();
        
        StringContent? content = null;
        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        var response = await _httpClient.PutAsync(BuildUri(modules, resource), content);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }
    
    public async Task<T> PutWhitData<T>(Modules modules, string resource, T? data)
    {
        await LoadToken();
        
        StringContent? content = null;
        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        var response = await _httpClient.PutAsync(BuildUri(modules, resource), content);
        return (await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<T>(), data, response))!;
    }

    public async Task<bool> PutPhotoAsync(Modules modules, string resource, HttpContent content)
    {
        await LoadToken();
        var response = await _httpClient.PutAsync(BuildUri(modules, resource), content);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }

    private async Task LoadToken()
    {
        var tokenResult = await _localStorage.GetAsync<string>(Utilities.Utilities.TokenKey);
        var token = tokenResult.Value;

        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token?.Trim())
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }

    protected virtual async Task<T> GenericHttpResponse<T>(Func<Task<T?>> httpRequest, T defaultResult,
        HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return (await httpRequest())!;
        }
        
        var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();
        throw new InternalException("Lo sentimos, ocurrió un error.",
            $"{problem?.Detail}",  problem!.GetValidationErrors(), (int)response.StatusCode);
    }

    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api?.Trim()))
            throw new InternalException("API environment variable not found.");

        return new Uri($"{api}/v7");
    }

    private static Task<bool> AlwaysTrue() => Task.Delay(0).ContinueWith(_ => true);
}