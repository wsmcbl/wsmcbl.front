using wsmcbl.front.Models;

namespace wsmcbl.front.Controllers;

public class StudentController
{
    private readonly HttpClient _httpClient;

    public StudentController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<StudentEntity>> GetStudentsFromApiAsync()
    {
        // Realizar solicitud HTTP GET a la API
        var response = await _httpClient.GetAsync("http://cblback.somee.com/v1/accounting/students");

        // Verificar si la solicitud fue exitosa
        if (response.IsSuccessStatusCode)
        {
            // Deserializar la respuesta JSON en una lista de StudentEntity
            return await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
        }
        else
        {
            // Manejar el error si la solicitud no fue exitosa
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
    }
}

