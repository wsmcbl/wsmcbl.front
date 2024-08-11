using System.Text;
using Newtonsoft.Json;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Service;
using wsmcbl.src.View.Secretary.Degrees.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using SubjectDto = wsmcbl.src.View.Secretary.SchoolYears.Dto.SubjectDto;

namespace wsmcbl.src.Controller;

public class CreateOfficialEnrollmentController(HttpClient httpClient)
{
    public async Task<List<SchoolYearDto>> GetSchoolYearsList()
    {
        var response = await httpClient.GetAsync(URL.ListSchoolYears);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<SchoolYearDto>>();
        }
        
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }
    
    public SchoolYearEntity NewSchoolYears()
    {
        var response = httpClient.GetAsync(URL.NewSchoolYear).Result;

        if (response.IsSuccessStatusCode)
        {
            return response.Content.ReadFromJsonAsync<SchoolYearEntity>().Result;
        }
    
        throw new Exception($"Error al obtener los datos de la API: {response.ReasonPhrase}");
    }
    
    public async Task<Response> SaveNewSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        try
        {
            NewSchoolYearDto newSchoolYearDto = MapperSchoolYear.MapToNewSchoolYearDto(schoolYearEntity);
            
            var url = URL.PostSchoolYear;
            var json = JsonConvert.SerializeObject(newSchoolYearDto); 
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            
            if (response.IsSuccessStatusCode)
            {
                return new Response { Success = true, Message = "Año Lectivo guardado con exito" };
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return new Response { Success = false, Message = $"Error del servidor: {errorContent}" };
            
        }
        catch (Exception e)
        {
            return new Response { Success = false, Message = $"An error occurred: {e.Message}" };
        }
    }
    
    public async Task<Response> CreateNewTariff(TariffDataDto tariffDto)
    {
        try
        {
            var url = URL.NewSchoolYearTariff;
            var json = JsonConvert.SerializeObject(tariffDto);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var respuesta = await httpClient.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                return new Response { Success = true, Message = "La tarifa fue guardada correctamente" };
            }
            var errorContent = await respuesta.Content.ReadAsStringAsync();
            return new Response { Success = false, Message = $"Error del servidor: {errorContent}" };
        }
        catch (Exception ex)
        {
            return new Response { Success = false, Message = $"An error occurred: {ex.Message}" };
        }
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
