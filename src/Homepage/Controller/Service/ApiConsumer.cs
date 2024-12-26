using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

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
            _ => ""
        };
        return new Uri($"{_server}/{moduleDir}/{resource.TrimStart('/')}");
    }

    public async Task<T> GetAsync<T>(Modules module, string resource, T defaultResult)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<T>(), defaultResult, response);
    }
    
    public async Task<TR> GetWithDtoAsync<T, TR>(Modules module, string resource, T requestDto, TR defaultResult)
    {
        var content = JsonContent.Create(requestDto);
        var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(module, resource))
        {
            Content = content
        };
        
        var response = await _httpClient.SendAsync(request);

        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<TR>(), defaultResult, response);
    }


    public async Task<byte[]> GetPdfAsync(Modules module, string resource)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadAsByteArrayAsync()!, [], response);
    }

    public async Task<R> PostAsync<T, R>(Modules modules, string resource, T data, R defaultResult)
    {
        var response = await _httpClient.PostAsJsonAsync(BuildUri(modules, resource), data);
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<R>(), defaultResult, response);
    }

    public async Task<bool> PutAsync<T>(Modules modules, string resource, T data)
    {
        var response = await _httpClient.PutAsJsonAsync(BuildUri(modules, resource), data);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }

    public async Task<bool> PutPhotoAsync(Modules modules, string resource, HttpContent content)
    {
        var response = await _httpClient.PutAsync(BuildUri(modules, resource), content);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }

    protected virtual async Task<T> GenericHttpResponse<T>(Func<Task<T?>> httpRequest, T defaultResult,
        HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return (await httpRequest())!;
        }
        
        return defaultResult;
    }

    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api?.Trim()))
            throw new Exception("API environment variable not found.");

        return new Uri($"{api}/v3");
    }

    private static Task<bool> AlwaysTrue() => Task.Delay(0).ContinueWith(_ => true);
}
