using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumer
{
    private readonly Notificator _notificator;
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly Uri _server;

    public ApiConsumer(Notificator notificator, HttpClient httpClient, ProtectedLocalStorage localStorage)
    {
        _notificator = notificator;
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
        await LoadToken();
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<T>(), defaultResult, response);
    }
    
    public async Task<byte[]> GetPdfAsync(Modules module, string resource)
    {
        await LoadToken();
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadAsByteArrayAsync()!, [], response);
    }

    public async Task<R> PostAsync<T, R>(Modules modules, string resource, T data, R defaultResult)
    {
        await LoadToken();
        var response = await _httpClient.PostAsJsonAsync(BuildUri(modules, resource), data);
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<R>(), defaultResult, response);
    }

    public async Task<bool> PutAsync<T>(Modules modules, string resource, T data)
    {
        await LoadToken();
        var response = await _httpClient.PutAsJsonAsync(BuildUri(modules, resource), data);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }
    
    public async Task<bool> PutPhotoAsync(Modules modules, string resource, HttpContent content)
    {
        await LoadToken();
        var response = await _httpClient.PutAsync(BuildUri(modules, resource), content);
        return await GenericHttpResponse(AlwaysTrue, false, response);
    }
    
    public async Task<string> LoginAsync(LoginDto data)
    {
        var defaultDto = new LoginDto();
        defaultDto.SetAsDefault();
        var response = await PostAsync(Modules.Config, "users/tokens", data, defaultDto);
        return response.token!;
    }
    
    private async Task LoadToken()
    {
        var tokenResult = await _localStorage.GetAsync<string>(Utilities.Utilities.TokenKey);
        var token = tokenResult.Value;

        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token?.Trim()) ? null :
            new AuthenticationHeaderValue("Bearer", token);
    }
    
    private async Task<T> GenericHttpResponse<T>(Func<Task<T?>> httpRequest, T defaultResult, HttpResponseMessage response)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                return (await httpRequest())!;
            }

            var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();

            throw new InternalException("Hubo un problema con la solicitud.",
                $"({problem!.Status}) {problem.Detail}", problem.GetValidationErrors());
        }
        catch (InternalException ex)
        {
            await _notificator.ShowError(ex.Title, ex.Content,ex.Details);
        }
        catch (Exception ex)
        {
            await _notificator.ShowError("Error interno.", $"{ex.Message} Trace: {ex.StackTrace}");
        }

        return defaultResult;
    }
    
    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api?.Trim()))
            throw new InternalException("API environment variable not found.");
        
        return new Uri($"{api}/v3");
    }
    
    private static Task<bool> AlwaysTrue() => Task.Delay(0).ContinueWith(_ => true);
}