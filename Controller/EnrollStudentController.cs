using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.Service;
using wsmcbl.front.View.Academy.Profiles;

namespace wsmcbl.front.Controller;

public class EnrollStudentController(HttpClient httpClient) : IEnrollSudentController
{
    public void GetStudents()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PostNewStudent(StudentDto student)
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