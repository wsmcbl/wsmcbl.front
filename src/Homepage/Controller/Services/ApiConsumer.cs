using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller.Services;

public class ApiConsumer
{
    private readonly HttpClient _httpClient;
    private readonly Uri _server;
    
    public ApiConsumer(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _server = GetServerUri();
    }
    
    public async Task<T> GetAsync<T>(Modules module, string resource, T defaultResult)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadFromJsonAsync<T>(), defaultResult, response);
    }
    
    public async Task<byte[]> GetByteFileAsync(Modules module, string resource)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadAsByteArrayAsync()!, [], response);
    }
    
    public async Task<(byte[]? Pdf, int StatusCode)> GetPdfAsync(Modules module, string resource)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        
        var statusCode = (int)response.StatusCode;
        if (!response.IsSuccessStatusCode)
        {
            return (null, statusCode);
        }
        
        var pdfBytes = await response.Content.ReadAsByteArrayAsync();
        return pdfBytes.Length > 0 ? (pdfBytes, statusCode) : (null, statusCode);
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
            $"{problem!.Errors}",  problem!.GetValidationErrors(), (int)response.StatusCode);
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
    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api?.Trim()))
            throw new Exception("API environment variable not found.");

        return new Uri($"{api}/v6");
    }
}