using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using SubjectDto = wsmcbl.src.View.Secretary.SchoolYears.Dto.SubjectDto;

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
    
    public async Task<SchoolYearEntity> GetNewSchoolYears(SchoolYearEntity Default)
    {
        var resource = "configurations/schoolyears?q=new";
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> SaveNewSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        var resource = "configurations/schoolyears";
        SchoolYearEntity Default = new(); 
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

    public async Task<bool> CreateNewSubject(SubjectDto subject)
    {
        var resource = "configurations/schoolyears/subjects";
        SubjectDto Default = new();
        var response = await Consumer.PostAsync(Modules.Secretary, resource, subject, Default);
        return response != Default;
    }

    public async Task<List<TeacherEntity>> GetTeacherBasic()
    {
        var resource = "teachers/";
        List<TeacherEntity> Default = new();
        return await Consumer.GetAsync(Modules.Secretary, resource, Default);
    }

    public async Task<EnrollmentEntity?> CreateEnrollments(string DegreeId, int Quantity)
    {
        (string, int) Data = (DegreeId, Quantity);
        var resource = "enrollments";
        EnrollmentEntity Default = new();
        return await Consumer.PostAsync(Modules.Secretary, resource, Data, Default);
    }

    public async Task<DegreeBasicDto?> ConfigureEnrollment(string GradeId)
    {
        var resource = "";
        
        
        var response = await httpClient.GetAsync(URL.ConfigurateEnrollment + $"{GradeId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
        }

        var degreeDto = await response.Content.ReadFromJsonAsync<DegreeBasicDto>();
        return degreeDto;
    }

    public async Task<List<DegreeEntity>> GetDegreeList()
    {
        var response = await httpClient.GetAsync(URL.ConfigurateEnrollment);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException($"Error al obtener los datos de la API.\n Mensaje: {response.ReasonPhrase}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<List<DegreeToListDto>>();
        var degreeList = new List<DegreeEntity>();
        foreach (var item in result)
        {
            degreeList.Add(item.ToEntity());
        }

        return degreeList;
    }
    
    
    
}
