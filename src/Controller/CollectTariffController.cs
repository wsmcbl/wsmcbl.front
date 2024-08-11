using System.Text.Json;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly HttpClient _httpClient;
    private readonly ApiConsumer _apiConsumer;
    public CollectTariffController(HttpClient httpClient, ApiConsumer apiConsumer)
    {
        _httpClient = httpClient;
        _apiConsumer = apiConsumer;
        
        cashier = new CashierEntity("caj-ktinoco");
    }
    
    public async Task<List<TariffDto>> GetTariffs(string key, string value)
    {
        var response = await _httpClient.GetAsync($"{URL.accounting}tariffs/search?q={key}:{value}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TariffDto>>();
        }
        
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        
    }
    
    public async Task<List<TariffDto>> GetTariffsOverdue(string value)
    {
        var response = await _httpClient.GetAsync($"{URL.accounting}tariffs/search?q={value}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TariffDto>>();
        }
        
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        
    }
    
    public async Task<string> SendPay()
    {
        var url = URL.accounting + "transactions";

        var json = JsonSerializer.Serialize(cashier.getTransaction);

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
        var content = new StringContent(string.Empty);

        var response = await _httpClient.PutAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return true;
    }
    
    public async Task<List<StudentEntity>?> getStudentList()
    {
        var response = await _httpClient.GetAsync(URL.accounting+"students");

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
    }
    
    public async Task<StudentEntity> GetStudent(string studentId)
    {
        var response = await _httpClient.GetAsync(URL.accounting + $"students/{studentId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<StudentEntity>();
        }

        throw new Exception($"Error al obtener el estudiante con ID {studentId}");
    }
    
    private CashierEntity cashier;

    public void addDetail(List<TariffModalDto> tariffs, bool isApplyArrear)
    {
        cashier.initTransaction();
        cashier.addDetail(tariffs, isApplyArrear);
    }

    public void setStudent(StudentEntity studentEntity)
    {
        cashier.setStudent(studentEntity);
    }
    
 
    
    
    public async Task<List<StudentEntity>?> GetStudentList_Test()
    {
        return await _apiConsumer.GetAsync<List<StudentEntity>>(Modules.Accounting, "students1", []);
    }  
}