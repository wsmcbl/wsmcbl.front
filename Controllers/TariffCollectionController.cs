namespace wsmcbl.front.Controllers;

using System.Text.Json;
using wsmcbl.front.Models.Accounting;

public class TariffCollectionController 
{
    private readonly HttpClient _httpClient;
    // Constructor
    public TariffCollectionController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Tariff>> GetTariffs(string key, string value)
    {
        // Realizar solicitud HTTP GET a la API
        var response = await _httpClient.GetAsync($"{URL.ACCOUNTING}tariffs/search?q={key}:{value}");

        // Verificar si la solicitud fue exitosa
        if (response.IsSuccessStatusCode)
        {
            // Deserializar la respuesta JSON en una lista de StudentEntity
            return await response.Content.ReadFromJsonAsync<List<Tariff>>();
        }
        else
        {
            // Manejar el error si la solicitud no fue exitosa
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
    }

    public async Task<string> SendPay(Models.Accounting.Output.TransactionDto obj)
    {
        var url = URL.ACCOUNTING + "transactions";

        var json = JsonSerializer.Serialize<Models.Accounting.Output.TransactionDto>(obj);

        var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var respuesta = await _httpClient.PostAsync(url, contenido);

        if(!respuesta.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var respuestaContenido = await respuesta.Content.ReadAsStringAsync();

        using JsonDocument doc = JsonDocument.Parse(respuestaContenido);
        JsonElement root = doc.RootElement;
        return root.GetProperty("transactionId").GetString()!;
    }
    
    public async Task<InvoiceDto?> GetInvoice(string transactionId)
    {
        var invoices = await _httpClient.GetAsync(URL.TRANSACTION+transactionId);

        if (!invoices.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {invoices.ReasonPhrase}");
        }
        
        return await invoices.Content.ReadFromJsonAsync<InvoiceDto>();
    }


    public async Task<bool> ActiveArrears(int idtarrif)
    {
        var url = URL.APPLYARREARS+idtarrif;
        var content = new StringContent(string.Empty); // Crear un contenido vacío

        var response = await _httpClient.PutAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
            bool success = true;

            return success;
    }
    
    
    public async Task<List<StudentEntity>?> GetStudentsFromApiAsync()
    {
        var response = await _httpClient.GetAsync(URL.ACCOUNTING+"students");

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
    }

    public async Task<StudentEntity> GetStudentEntityAsync(string studentId)
    {
        var response = await _httpClient.GetAsync(URL.ACCOUNTING + $"students/{studentId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<StudentEntity>();
        }
        else
        {
            throw new Exception($"Error al obtener el estudiante con ID {studentId}");
        } 
    }

}