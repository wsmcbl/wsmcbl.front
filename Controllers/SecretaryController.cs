using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.dto.input;
using wsmcbl.front.dto.Output;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Controllers;

public class SecretaryController
{
    private readonly HttpClient _httpClient;
    public SecretaryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<SchoolYearDto>> GetSchoolYears()
    {
        var response = await _httpClient.GetAsync(URL.ListSchoolYears);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<SchoolYearDto>>();
        }
        
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }
    
    public async Task <SchoolYearEntity> NewSchoolYears()
    {
        var response = await _httpClient.GetAsync(URL.NewSchoolYear);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SchoolYearEntity>();
        }
        
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }

    public async Task<bool> SaveSchoolYear(NewSchoolYearDto schoolYearEntity)
    {
        var url = URL.PostSchoolYear; // Asumiendo que URL.PostSchoolYear es la URL base correcta

        // Serializar el objeto SchoolYearEntity usando Newtonsoft.Json
        var json = JsonConvert.SerializeObject(schoolYearEntity); 

        var contenido = new StringContent(json, Encoding.UTF8, "application/json");

        var respuesta = await _httpClient.PostAsync(url, contenido);

        // Devolver true si la respuesta fue exitosa, false en caso contrario
        return respuesta.IsSuccessStatusCode;
    }
    
    public async Task<ApiResponse> NewTariff(NewTariffDto tariffs)
    {
        try
        {
            var url = URL.NewSchoolYearTariff;
            var json = JsonConvert.SerializeObject(tariffs);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                return new ApiResponse { Success = true, Message = "La tarifa fue guardada correctamente" };
            }
            else
            {
                var errorContent = await respuesta.Content.ReadAsStringAsync();
                return new ApiResponse { Success = false, Message = $"Error del servidor: {errorContent}" };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse { Success = false, Message = $"An error occurred: {ex.Message}" };
        }
    }



}
