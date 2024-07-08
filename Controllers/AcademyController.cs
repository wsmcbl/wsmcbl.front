using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.model.Secretary.Output;
using wsmcbl.front.Models.Secretary.Input;

namespace wsmcbl.front.Controllers;

public class AcademyController(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<StudentEntity>?> GetStudents()
    {
        var response = await _httpClient.GetAsync(URL.secretary + "students");

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
    }
    
    public async Task<bool> PostNewStudent(StudentEntityDto student)
    {
        var url = URL.secretary+"students";

        var json = JsonConvert.SerializeObject(student);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return true;
    }
    
    
}
