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

    public async Task<Response> CreateNewSubject(SubjectDto subject)
    {
        try
        {
            var url = URL.NewSubject;
            var json = JsonConvert.SerializeObject(subject);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await httpClient.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                return new Response { Success = true, Message = "Asignatura creada correctamente" };
            }
            var errorContent = await respuesta.Content.ReadAsStringAsync();
            return new Response { Success = false, Message = $"Error del servidor: {errorContent}" };
        }
        catch (Exception ex)
        {
            return new Response { Success = false, Message = $"Ocurrió un error: {ex.Message}" };
        }
    }

    public async Task<List<TeacherEntity>> GetTeacherBasic()
    {
        var response = await httpClient.GetAsync(URL.GetTeacherBasic);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException($"Error al obtener los datos de la API.\n Mensaje: {response.ReasonPhrase}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<List<TeacherBasicDto>>();
        var teacherList = new List<TeacherEntity>();
        foreach (var item in result)
        {
            teacherList.Add(item.ToEntity());
        }

        return teacherList;
    }

    public async Task<Response> CreateEnrollments(string DegreeId, int Quantity)
    {
        try
        {
            EnrollmentToCreateDto obj = new(DegreeId, Quantity);
            
            var url = URL.DegreesEnrollments;
            var json = JsonConvert.SerializeObject(obj);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var respuesta = await httpClient.PostAsync(url, contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                return new Response { Success = true, Message = "Secciòn creada correctamente" };
            }
            var errorContent = await respuesta.Content.ReadAsStringAsync();
            return new Response { Success = false, Message = $"Error del servidor: {errorContent}" };
        }
        catch (Exception e)
        {
            return new Response { Success = false, Message = $"Ocurrió un error: {e.Message}" };
        }
    }

    public async Task<DegreeBasicDto?> ConfigureEnrollment(string GradeId)
    {
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
