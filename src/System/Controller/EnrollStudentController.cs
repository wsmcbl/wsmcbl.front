using Newtonsoft.Json;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using JsonSerializer = System.Text.Json.JsonSerializer;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public class EnrollStudentController(ApiConsumer consumer) : IEnrollStudentController
{
    public async Task<List<StudentDto>> GetStudents()
    {
        List<StudentDto> defaultResult = [];
        return await consumer.GetAsync(Modules.Secretary, "enrollments/students", defaultResult);
    }
    
    public async Task<List<DegreeBasicDto>> GetDegreeBasicList()
    {
        List<DegreeBasicDto> defaultResult = [];
        return await consumer.GetAsync(Modules.Secretary, "enrollments/degrees", defaultResult);
    }

    public async Task<bool> SaveEnrollment(StudentEntity student, string enrollmentId, int discountId)
    {
       
       
        
        /////////////////////////////////////////////////////////////////////////////////////////
        var content = student.ToEnrollStudentDto(enrollmentId, discountId);
        var json = JsonSerializer.Serialize(content);
        return await consumer.PutAsync(Modules.Secretary, "enrollments", content);
    }

    public async Task<(StudentEntity student, string? enrollmentId, int discountId)> GetInfoStudent(string studentId)
    {
        var resource = $"enrollments/students/{studentId}";
        EnrollStudentDto defaultResult = new();
        var result = await consumer.GetAsync(Modules.Secretary, resource, defaultResult);
        
        return (result.ToStudentEntity(), result.enrollmentId, result.discountId);
    }
    
    public async Task<byte[]> GetEnrollSheetPdf(string studentId)
    {
        var resource = $"enrollments/documents/{studentId}";
        return await consumer.GetPdfAsync(Modules.Secretary, resource);
    }
}