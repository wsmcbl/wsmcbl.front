using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.Controller;

public class UpdateOfficialEnrollmentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public UpdateOfficialEnrollmentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<TeacherEntity>> GetActiveTeacherList()
    {
        List<TeacherEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy, "teachers?q=active", defaultResult);
    }
    
    public async Task<List<TeacherEntity>> GetNonGuidedTeacherList()
    {
        List<TeacherEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy, "teachers?q=non-guided", defaultResult);
    }
    
    public async Task<bool> UpdateTeacherGuide(string enrollmentId, string teacherId)
    {
        var resource = $"enrollments/{enrollmentId}/teachers?teacherId={teacherId}";
        return await _apiConsumer.PutAsync<object>(Modules.Academy, resource, null);
    }
    
    public async Task<bool> UpdateTeacherSubject(string enrollmentId, string subjectId, string teacherId)
    {
        var resource = $"enrollments/{enrollmentId}/subjects/{subjectId}?teacherId={teacherId}";
        return await _apiConsumer.PutAsync<object>(Modules.Academy, resource, null);
    }    
    
    public async Task<EnrollmentSubjectListDto> GetEnrollmentListByDegreeId(string degreeId)
    {
        var resource = $"degrees/{degreeId}/enrollments";
        EnrollmentSubjectListDto Default = new();
        return await _apiConsumer.GetAsync(Modules.Academy, resource, Default);
    }

    public async Task<bool> UpdateEnrollmentList(List<EnrollmentEntity> enrollmentList)
    {
        foreach (var item in enrollmentList)
        {
            var dto = new UpdateEnrollmentDto(item);
            var result = await _apiConsumer.PutAsync(Modules.Academy, $"enrollments/{item.enrollmentId}", dto);
            if (!result)
            {
                return false;
            }
        }

        return true;
    }
}
