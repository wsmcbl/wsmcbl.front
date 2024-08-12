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
        _connectionString = "http://185.190.140.208:4000/v1";
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

    public async Task<T> GetAsync<T>(Modules modules, string resource, T defaultResult)
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
            await service.ShowError("Hubo un problema con la solicitud.", ex.Message);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }
        
        return result;
    }
    
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(Modules modules, string resource, TRequest data, TResponse defaultResult)
    {
        var result = defaultResult;
        try
        {
            var response = await httpClient.PostAsJsonAsync(BuildUri(modules, resource), data);
            response.EnsureSuccessStatusCode();
        
            result = await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (HttpRequestException ex)
        {
            await service.ShowError("Hubo un problema con la solicitud.", ex.Message);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }

        return result;
    }

    public async Task PutAsync<T>(Modules modules, string resource, T data)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(BuildUri(modules, resource), data);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            await service.ShowError("Hubo un problema con la solicitud.", ex.Message);
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }
    }
    
    public async Task<byte[]?> GetPdfAsync(Modules module, string resource, byte[] defaultResult)
    {
        byte[] result = defaultResult;
        try
        {
            var response = await httpClient.GetAsync(BuildUri(module, resource));
            response.EnsureSuccessStatusCode();
            
            result = await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            await service.ShowError("Error interno.", ex.Message);
        }

        return result;
    }
}