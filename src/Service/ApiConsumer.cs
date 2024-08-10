using Newtonsoft.Json;

namespace wsmcbl.src.Service;

public class ApiConsumer
{
    private readonly HttpClient client;
    private readonly ErrorService error;
    private readonly string _connectionString;

    public ApiConsumer(HttpClient client, ErrorService error)
    {
        this.client = client;
        this.error = error;
        _connectionString = "http://185.190.140.208:4000/v1";
    }
    
    private string BuildUri(Resources module, string resource)
    {
        var moduleDir = module switch
        {
            Resources.Academy => "academy",
            Resources.Secretary => "secretary",
            Resources.Accounting => "accounting",
            _ => ""
        };

        return $"{_connectionString}/{moduleDir}/{resource.TrimStart('/')}";
    }

    public async Task<T> GetAsync<T>(Resources module, string resource)
    {
        try
        {
            var url = BuildUri(module, resource);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            return result!;
        }
        catch (Exception e)
        {
            error.ShowError(e.Message);
            return default;
        }
    }
    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(Resources module, string resource, TRequest data)
    {
        var url = BuildUri(module, resource);
        var response = await client.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(Resources module, string resource, T data)
    {
        var url = BuildUri(module, resource);
        var response = await client.PutAsJsonAsync(url, data);
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