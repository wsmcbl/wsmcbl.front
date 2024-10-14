using System.Text.Json;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public class EnrollStudentController(ApiConsumer consumer) : IEnrollStudentController
{
    public async Task<List<StudentDto>> GetStudents()
    {
        var resource = "enrollments/students";
        List<StudentDto> defaultResult = [];
        return await consumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    
    public async Task<List<DegreeBasicDto>> GetDegreeBasicList()
    {
        var resource = "enrollments/degrees";
        List<DegreeBasicDto> defaultResult = [];
        return await consumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }

    public async Task<bool> SaveEnrollment(StudentEntity student, string enrollmentId)
    {
        var content = MapperStudent.MapToEnrollmentDto(student, enrollmentId);
        
        string jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
        
        var result = await consumer.PutAsync(Modules.Secretary, "enrollments", content);
        return result;
    }

    public async Task<StudentEntity> GetInfoStudent(string studentId)
    {
        var resource = $"enrollments/students/{studentId}";
        StudentFullDto defaultResult = new();
        var studentResult = await consumer.GetAsync(Modules.Secretary, resource, defaultResult);
        
        return MapperStudent.MapToEntity(studentResult);
    }
    
    public async Task<byte[]?> GetPdfContent(string studentId)
    {
        var resource = $"enrollments/documents/{studentId}";
        var content = await consumer.GetPdfAsync(Modules.Secretary, resource);
        return content;
    }
}