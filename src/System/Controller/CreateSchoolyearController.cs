using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.Controller;

public class CreateSchoolyearController : BaseController
{
    public CreateSchoolyearController(ApiConsumerFactory apiFactory) : base(apiFactory, "schoolyears")
    {
    }
    
    public async Task<List<SchoolYearDto>> GetSchoolyearList()
    {
        List<SchoolYearDto> Default = [];
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
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