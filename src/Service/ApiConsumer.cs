using Newtonsoft.Json;

namespace wsmcbl.src.Service;

public class ApiConsumer
{
    private readonly HttpClient httpClient;
    private readonly string _connectionString;

    public ApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        _connectionString = "http://185.190.140.208:4000/v1";
    }
    
    private string? BuildUri(ModuleEnum module, string resource)
    {
        var moduleDir = module switch
        {
            ModuleEnum.Academy => "academy",
            ModuleEnum.Secretary => "secretary",
            ModuleEnum.Accounting => "accounting",
            _ => ""
        };

        return $"{_connectionString}/{moduleDir}/{resource.TrimStart('/')}";
    }

    public async Task<T?> GetAsync<T>(ModuleEnum module, string resource)
    {
        try
        {
            var response = await httpClient.GetAsync(BuildUri(module, resource));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Mala petición");
        }
    }
    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(ModuleEnum module, string resource, TRequest data)
    {
        var url = BuildUri(module, resource);
        var response = await httpClient.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(ModuleEnum module, string resource, T data)
    {
        var url = BuildUri(module, resource);
        var response = await httpClient.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
    }
    
    private static StringContent getContent(string resource)
    {
        return new StringContent(resource, System.Text.Encoding.UTF8, "application/json");
    }

    protected static StringContent getContentByDto(object? entity)
    {
        return getContent(JsonConvert.SerializeObject(entity));
    }

    protected static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);

}