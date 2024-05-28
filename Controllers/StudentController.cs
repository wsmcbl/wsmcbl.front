namespace wsmcbl.front.Controllers;

using Microsoft.AspNetCore.SignalR;
using wsmcbl.front.Models.Accounting;

public class StudentController
{
    private readonly HttpClient _httpClient;

    public StudentController(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

