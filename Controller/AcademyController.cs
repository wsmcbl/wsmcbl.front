using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.model.Secretary.Input;
using wsmcbl.front.model.Secretary.Output;
using wsmcbl.front.Service;

namespace wsmcbl.front.Controller;

public class AcademyController(HttpClient httpClient)
{
    public async Task<List<StudentEntity>?> GetStudents()
    {
        var response = await httpClient.GetAsync(URL.secretary + "students");

        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<List<wsmcbl.front.model.Secretary.Input.StudentEntity>>();
    }
    
    public async Task<bool> PostNewStudent(StudentEntityDto student)
    {
        var url = URL.secretary+"students";

        var json = JsonConvert.SerializeObject(student);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return false;
    }
}
