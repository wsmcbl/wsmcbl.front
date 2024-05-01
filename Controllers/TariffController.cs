namespace wsmcbl.front.Controllers;

using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;

using wsmcbl.front.Models.Accounting;

public class TariffController 
{
    private readonly HttpClient _httpClient;
    // Constructor
    public TariffController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Tariff>> GetTariffs()
    {
        // Realizar solicitud HTTP GET a la API
        var response = await _httpClient.GetAsync("http://wsmcbl-api.somee.com/v1/accounting/tariffs");

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

    public async Task<HttpResponseMessage> SendPay(TransactionDto obj)
    {
        // URL a la que enviar la solicitud PUT
        var url = "http://wsmcbl-api.somee.com/v1/accounting/transactions";

        // Convertir el objeto a JSON
        var json = JsonSerializer.Serialize<TransactionDto>(obj);
        
        // Crear el contenido de la solicitud HTTP
        var contenido = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        // Realizar la solicitud PUT
        var respuesta = await _httpClient.PostAsync(url, contenido);

        return respuesta;
    }

}