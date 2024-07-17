using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Controllers;

public class SecretaryController
{
    private readonly HttpClient _httpClient;
    public SecretaryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<GradeEntity?>> GetGrades()
    {
        var response = await _httpClient.GetAsync(URL.secretary + "grades");
        
        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        return await response.Content.ReadFromJsonAsync<List<GradeEntity>>();
    }
    
    public async Task<List<Subject?>> GetSubjects()
    {
        var response = await _httpClient.GetAsync(URL.secretary + "grades/subjects");
        
        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        return await response.Content.ReadFromJsonAsync<List<Subject>>();
    }
}
