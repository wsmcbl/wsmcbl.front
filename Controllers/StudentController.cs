namespace wsmcbl.front.Controllers;

using Microsoft.AspNetCore.SignalR;
using wsmcbl.front.Models.Accounting;


public class StudentController
{
    private readonly HttpClient _httpClient;

    // Constructor
    public StudentController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    // Get Student List from 
    public async Task<List<StudentEntity>> GetStudentsFromApiAsync()
    {
        // Realizar solicitud HTTP GET a la API
        var response = await _httpClient.GetAsync("http://wsmcbl-api.somee.com/v1/accounting/students");

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

    // Get Info Student for tariff Collections
    public async Task<StudentEntity> GetStudentEntityAsync(string studentId)
    {
        
        var response = await _httpClient.GetAsync($"http://wsmcbl-api.somee.com/v1/accounting/students/{studentId}");

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

