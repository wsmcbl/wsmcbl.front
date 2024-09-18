using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

namespace wsmcbl.src.Controller;

public class EnrollStudentController : IEnrollStudentController
{
    private HttpClient httpClient; //Esto desaparece
    private ApiConsumer Consumer;

    public EnrollStudentController(HttpClient httpClient, ApiConsumer consumer)
    {
        Consumer = consumer;
        this.httpClient = httpClient;
    }

    public async Task<List<StudentDto>> GetStudents()
    {
        var resource = "enrollments/students";
        List<StudentDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    
    public async Task<List<DegreeBasicDto>> GetDegreeBasicList()
    {
        var resource = "enrollments/degrees";
        List<DegreeBasicDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }

    public async Task<StudentFullDto> GetInfoStudent(string StudentID)
    {
        var resource = $"enrollments/students/{StudentID}";
        StudentFullDto defaultResult = new();
        
        var studentResult = await Consumer.GetAsync(Modules.Secretary, resource, defaultResult);

        studentResult.birthday ??= DateTime.Today;

        return studentResult;
    }
    
    public async Task<byte[]?> GetPdfContent(string studentId)
    {
        var resource = $"enrollments/documents/{studentId}";
        byte[] defaultResult = [];
        var content = await Consumer.GetPdfAsync(Modules.Secretary, resource, defaultResult);
        return content;
    }
}