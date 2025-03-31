using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class MoveStudentFromEnrollmentController : BaseController
{
    public MoveStudentFromEnrollmentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<bool> MoveStudent(string studentId, string enrollmentId)
    {
        var resource = $"{path}?studentId={studentId}&enrollmentId={enrollmentId}";
        
        return await apiFactory.WithNotificator.PutAsync(Modules.Academy, resource, new EnrollStudentDto());
    }

    public async Task<List<DegreeBasicDto>?> GetDegreeBasicList(string studentId)
    {
        var controller = new EnrollStudentController(apiFactory);
        return await controller.GetDegreeBasicList(studentId);
    }
}