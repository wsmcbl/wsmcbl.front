using System.Text.Json;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.Controller;

public class CreateOfficialEnrollmentController
{
    private readonly ApiConsumer Consumer;

    public CreateOfficialEnrollmentController(ApiConsumer consumer)
    {
        Consumer = consumer;
    }
    
    public async Task<List<SchoolYearDto>> GetSchoolYearsList()
    {
        var resource = "configurations/schoolyears?q=all";
        List<SchoolYearDto> Default = [];
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<Model.Secretary.SchoolYearEntity> GetNewSchoolYears(Model.Secretary.SchoolYearEntity Default)
    {
        var resource = "configurations/schoolyears?q=new";
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> SaveNewSchoolYear(Model.Secretary.SchoolYearEntity schoolYearEntity, List<PartialListDto> partials)
    {
        var resource = "configurations/schoolyears";
        Model.Secretary.SchoolYearEntity Default = new(); 
        var content = new CreateSchoolYearDto(schoolYearEntity, partials);
        var response = await Consumer.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }

    public async Task<bool> CreateNewTariff(SchoolyearTariffDto schoolyearTariff)
    {
        var resource = "configurations/schoolyears/tariffs";
        TariffDataDto Default = new();
        var content = MapperDate.MapToTariffDataDto(schoolyearTariff);
        var response = await Consumer.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }

    public async Task<bool> CreateNewSubject(View.Secretary.SchoolYears.Dto.SubjectDto subject)
    {
        var resource = "configurations/schoolyears/subjects";
        View.Secretary.SchoolYears.Dto.SubjectDto Default = new();
        var response = await Consumer.PostAsync(Modules.Secretary, resource, subject, Default);
        return response != Default;
    }

    public async Task<List<TeacherEntity>> GetTeacherList()
    {
        var resource = "teachers/";
        List<TeacherEntity> Default = [];
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }

    public async Task<EnrollmentEntity?> CreateEnrollments(string degreeId, int quantity, EnrollmentEntity Default)
    {
        var data = new PostDegreeDto { degreeId = degreeId, quantity = quantity };
        return await Consumer.PostAsync(Modules.Secretary, "degrees/enrollments", data, Default);
    }

    public async Task<DegreeEntity> GetConfigureEnrollment(string GradeId)
    {
        var resource = $"degrees/{GradeId}";
        DegreeBasicDto Default = new();
        var result = await Consumer.GetAsync(Modules.Secretary, resource, Default);

        var teacherList = await Consumer.GetAsync<List<TeacherEntity>>(Modules.Secretary, "teachers", []);
        result.SetTeacherList(teacherList);

        return result.toEntity();
    }

    public async Task<List<DegreeEntity>> GetDegreeList()
    {
        List<DegreeEntity> Default = [];
        return await Consumer.GetAsync(Modules.Secretary, "degrees", Default);
    }

    public async Task<List<DropdownList>> GetTypeTariffList()
    {
        var resource = "tariffs/types";
        List<TypeTariffDto> Default = [];
        var response = await Consumer.GetAsync(Modules.Accounting, resource, Default);
        return response.Select(dto => dto.ToDropdownList()).ToList();
    }

    public async Task<bool> PutSaveEnrollment(DegreeEntity degree)
    {
        var contentList = degree.MapToListDto();
        
        if (contentList.Count <= 0)
        {
            return true;
        }
     
        var result = true;   
        foreach (var content in contentList)
        {
            result = await Consumer.PutAsync(Modules.Secretary, "degrees/enrollments", content);
        }

        return result;
    }

}
