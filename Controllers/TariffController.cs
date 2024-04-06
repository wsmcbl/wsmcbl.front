namespace wsmcbl.front.Controllers;
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

}