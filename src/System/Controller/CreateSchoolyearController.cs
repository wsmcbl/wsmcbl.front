using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.Schoolyear.Dto;

namespace wsmcbl.src.Controller;

public class CreateSchoolyearController : BaseController
{
    public CreateSchoolyearController(ApiConsumerFactory apiFactory) : base(apiFactory, "schoolyears")
    {
    }
    
    public async Task<List<BasicSchoolyearDto>> GetSchoolyearList()
    {
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource,new List<BasicSchoolyearDto>());
    }
    
    public async Task<Model.Secretary.SchoolYearEntity> GetNewSchoolYears(Model.Secretary.SchoolYearEntity Default)
    {
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> CreateSchoolyear(Model.Secretary.SchoolYearEntity schoolYearEntity, List<PartialListDto> partials)
    {
        Model.Secretary.SchoolYearEntity Default = new(); 
        var content = new CreateSchoolYearDto(schoolYearEntity, partials);
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }
}