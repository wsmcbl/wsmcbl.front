using Newtonsoft.Json;

namespace wsmcbl.src.Utilities;

public class ApiConsumer
{
    private readonly HttpClient httpClient;
    private readonly string _connectionString;

    public ApiConsumer(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        _connectionString = "http://185.190.140.208:4000/v1";
    }
    
    private string? BuildUri(Modules modules, string resource)
    {
        var moduleDir = modules switch
        {
            Modules.Academy => "academy",
            Modules.Secretary => "secretary",
            Modules.Accounting => "accounting",
            _ => ""
        };

        return $"{_connectionString}/{moduleDir}/{resource.TrimStart('/')}";
    }

    public async Task<T?> GetAsync<T>(Modules modules, string resource, T defaultResult)
    {
        var result = defaultResult; 
        try
        {
            var response = await httpClient.GetAsync(BuildUri(modules, resource));
            response.EnsureSuccessStatusCode();

            result = await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            throw new InternalException("Hubo un problema con la solicitud.", ex.Message);
        }
        catch (Exception ex)
        {
            throw new InternalException(ex.Message);
        }
        
        return result;
    }
    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(Modules modules, string resource, TRequest data)
    {
        var url = BuildUri(modules, resource);
        var response = await httpClient.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(Modules modules, string resource, T data)
    {
        var url = BuildUri(modules, resource);
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