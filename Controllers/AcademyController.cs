namespace wsmcbl.front.Controllers.AcademyController;


using wsmcbl.front.Models.Secretary.Input;
using wsmcbl.front.Controllers;


public class AcademyController(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<StudentEntity>?> GetStudents()
    {
        var response = await _httpClient.GetAsync(URL.SECRETARY + "students");

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
    }
}
