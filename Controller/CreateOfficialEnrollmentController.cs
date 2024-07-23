using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.dto.Output;
using wsmcbl.front.Model.Secretary.Input;
using wsmcbl.front.Service;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.Controller;

public class CreateOfficialEnrollmentController
{
    private readonly HttpClient _httpClient;
    public CreateOfficialEnrollmentController(HttpClient httpClient)
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
    
    public SchoolYearEntity NewSchoolYears()
    {
        var response = _httpClient.GetAsync(URL.NewSchoolYear).Result;

        if (response.IsSuccessStatusCode)
        {
            return response.Content.ReadFromJsonAsync<SchoolYearEntity>().Result;
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
    
    public async Task<ApiResponse> CreateNewTariff(TariffDataDto tariffDto)
    {
        try
        {
            var url = URL.NewSchoolYearTariff;
            var json = JsonConvert.SerializeObject(tariffDto);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await GetResponse(url, contenido);

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

    private async Task<HttpResponseMessage> GetResponse(string url, StringContent content)
        => await _httpClient.PostAsync(url, content);
}
