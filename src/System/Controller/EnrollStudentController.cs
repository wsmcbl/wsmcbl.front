using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public class EnrollStudentController
{
    private ApiConsumerWithNotificator _apiConsumer;
    
    public EnrollStudentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<StudentDto>> GetStudents()
    {
        List<StudentDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "enrollments/students", defaultResult);
    }
    
    public async Task<List<DegreeBasicDto>> GetDegreeBasicList()
    {
        List<DegreeBasicDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "enrollments/degrees", defaultResult);
    }

    public async Task<bool> SaveEnrollment(StudentEntity student, string enrollmentId, int discountId, bool isRepeating)
    {
        var content = student.ToEnrollStudentDto(enrollmentId, discountId, isRepeating);
        return await _apiConsumer.PutAsync(Modules.Secretary, "enrollments", content);
    }

    public async Task<(StudentEntity student, string? enrollmentId, int discountId, bool isRepeating)> GetStudentById(string studentId)
    {
        var resource = $"enrollments/students/{studentId}";
        EnrollStudentDto defaultResult = new();
        var result = await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);
        
        return (result.ToStudentEntity(), result.enrollmentId, result.discountId, result.isRepeating);
    }
    
    public async Task<byte[]> GetEnrollSheetPdf(string studentId)
    {
        var resource = $"enrollments/documents/{studentId}";
        return await _apiConsumer.GetPdfAsync(Modules.Secretary, resource);
    }
    
    public async Task<bool> UpdateEnrollmet(string studentId, string enrollmentId)
    {
        EnrollStudentDto defaultResult = new();
        return await _apiConsumer.PutAsync(Modules.Academy, $"students?studentId={studentId}&enrollmentId={enrollmentId}", defaultResult);
    }
    
}