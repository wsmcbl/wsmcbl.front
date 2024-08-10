using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using wsmcbl.src.Service;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using StudentDto = wsmcbl.src.View.Academy.Profiles.StudentDto;

namespace wsmcbl.src.Controller;

public class EnrollStudentController: IEnrollSudentController
{
    private readonly HttpClient _httpClient;
    private readonly ApiConsumer _apiConsumer;
    public EnrollStudentController(HttpClient httpClient, ApiConsumer apiConsumer)
    {
        _httpClient = httpClient;
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>> GetStudents()
    {
        var response = await _httpClient.GetAsync(URL.EnrollStudentList);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>>();
        }
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }

    public async Task<StudentFullDto> GetInfoStudent(string studentId)
    {
        var student = await _apiConsumer.GetAsync<StudentFullDto>(Resources.Secretary, $"enrollments/students/{studentId}");
        return student;
    }
    
    public async Task<byte[]?> GetPdfContent(string studentId)
    {
        var content = await _apiConsumer.GetPdfAsync(Resources.Secretary, $"enrollments/documents/{studentId}");
        return content;
    }
    
    public async Task<bool> PostNewStudent(StudentDto student)
    {
        var url = URL.secretary+"students";

        var json = JsonConvert.SerializeObject(student);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }
        
        return false;
    }
}