using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Controller;

public class CreateOfficialEnrollmentController
{
    private ApiConsumer Consumer;

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
    
    public async Task<bool> SaveNewSchoolYear(Model.Secretary.SchoolYearEntity schoolYearEntity)
    {
        var resource = "configurations/schoolyears";
        Model.Secretary.SchoolYearEntity Default = new(); 
        var content = MapperSchoolYear.MapToNewSchoolYearDto(schoolYearEntity);
        var response = await Consumer.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }

    public async Task<bool> CreateNewTariff(SchoolYearTariffs tariff)
    {
        var resource = "configurations/schoolyears/tariffs";
        TariffDataDto Default = new();
        var content = MapperDate.MapToTariffDataDto(tariff);
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
        PostDegreeDto data = new PostDegreeDto { degreeId = degreeId, quantity = quantity };
        var resource = "degrees/enrollments";
        return await Consumer.PostAsync(Modules.Secretary, resource, data, Default);
    }

    public async Task<DegreeEntity> GetConfigureEnrollment(string GradeId)
    {
        var resource = $"degrees/{GradeId}";
        DegreeBasicDto Default = new();
        var result = await Consumer.GetAsync(Modules.Secretary, resource, Default);

        return result.toEntity();
    }

    public async Task<List<DegreeEntity>> GetDegreeList()
    {
        var resource = "degrees/";
        List<DegreeEntity> Default = [];
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }

    public async Task<List<DropdownList>> GetTypeTariffList()
    {
        var resource = "tariffs/types";
        List<TypeTariffDto> Default = [];
        var response = await Consumer.GetAsync(Modules.Accounting, resource, Default);
        return response.Select(dto => dto.ToDropdownList()).ToList();
    }

    public async Task<bool> PutSaveEnrollment(DegreeEntity Degree, DegreeEntity Default)
    {
        var resource = "degrees/enrollments";
        var content = CreateEnrollmentsDto.MaptoCreateEnrollmentsDto(Degree);
        await Consumer.PutAsync(Modules.Secretary, resource, content[0]);
        return Degree.Equals(Default); 
    }
}
