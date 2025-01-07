namespace wsmcbl.homepage.Controller.Services;

public class ApiConsumer
{
    private readonly HttpClient _httpClient;
    private readonly Uri _server;
    
    public ApiConsumer(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
    private static Uri GetServerUri()
    {
        var api = Environment.GetEnvironmentVariable("API");
        if (string.IsNullOrEmpty(api?.Trim()))
            throw new Exception("API environment variable not found.");

        return new Uri($"{api}/v4");
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
    
    
    public async Task<byte[]> GetPdfAsync(Modules module, string resource)
    {
        var response = await _httpClient.GetAsync(BuildUri(module, resource));
        return await GenericHttpResponse(() => response.Content.ReadAsByteArrayAsync()!, [], response);
    }
    
    

}