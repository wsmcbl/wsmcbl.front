using System.Text;
using Newtonsoft.Json;
using wsmcbl.front.Service;
using wsmcbl.front.View.Academy.Profiles;
using wsmcbl.front.View.Secretary.EnrollmentStudent.Dto;
using StudentDto = wsmcbl.front.View.Academy.Profiles.StudentDto;

namespace wsmcbl.front.Controller;

public class EnrollStudentController(HttpClient httpClient) : IEnrollSudentController
{
    public async Task<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>> GetStudents()
    {
        var response = await httpClient.GetAsync(URL.EnrollStudentList);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>>();
        }
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }

    public async Task<StudentFullDto> GetInfoStudent(string studentId)
    {
        try
        {
            var response = await httpClient.GetAsync($"{URL.GetInfoStudent}{studentId}");
            if (response.IsSuccessStatusCode)
            {
                var studentResult =  await response.Content.ReadFromJsonAsync<StudentFullDto>();

                if (studentResult.birthday == null)
                {
                    studentResult.birthday.Year = DateTime.Today.Year;
                    studentResult.birthday.Month = DateTime.Today.Month;
                    studentResult.birthday.Day = DateTime.Today.Day;
                }
                
                return studentResult;
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error del servidor.");
        }
        
        throw new ArgumentException("Error al obtener los datos del estudiante.", nameof(studentId));
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