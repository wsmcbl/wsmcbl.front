using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
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

    public async Task<bool> SaveEnrollment(StudentEntity student, string enrollmentId)
    {
        var content = MapperStudent.MapToEnrollmentDto(student, enrollmentId);
        return await consumer.PutAsync(Modules.Secretary, "enrollments", content);
    }

    public async Task<StudentEntity> GetInfoStudent(string studentId)
    {
        var resource = $"enrollments/students/{studentId}";
        StudentFullDto defaultResult = new();
        var studentResult = await consumer.GetAsync(Modules.Secretary, resource, defaultResult);
        
        return MapperStudent.MapToEntity(studentResult);
    }
    
    public async Task<byte[]> GetEnrollSheetPdf(string studentId)
    {
        var resource = $"enrollments/documents/{studentId}";
        return await consumer.GetPdfAsync(Modules.Secretary, resource);
    }
}