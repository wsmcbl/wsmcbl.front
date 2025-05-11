using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public class EnrollStudentController : BaseController
{
    public EnrollStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "enrollments/students")
    {
    }
    
    public async Task<List<StudentDto>> GetStudentList()
    {
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, path, new List<StudentDto>());
    }
    
    public async Task<List<DegreeBasicDto>> GetDegreeBasicList(string studentId)
    {
        var resource = $"{path}/{studentId}/degrees";
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, resource, new List<DegreeBasicDto>());
    }

    public async Task<bool> SaveEnroll(StudentEntity student, string enrollmentId, int discountId, bool isRepeating)
    {
        var content = student.ToEnrollStudentDto(enrollmentId, discountId, isRepeating);
        
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, "enrollments", content);
    }

    public async Task<(StudentEntity student, string? enrollmentId, int discountId, bool isRepeating)> GetStudentById(string studentId)
    {
        var resource = $"{path}/{studentId}";
        
        var result = await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, resource, new EnrollStudentDto());
        
        return (result.ToStudentEntity(), result.enrollmentId, result.discountId, result.isRepeating);
    }
    
    public async Task<byte[]> GetEnrollSheetPdf(string studentId)
    {
        var resource = $"{path}/{studentId}/export";
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Secretary, resource);
    }
}