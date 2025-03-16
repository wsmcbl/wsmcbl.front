using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.Controller;

public class CreateSchoolyearController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public CreateSchoolyearController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<SchoolYearDto>> GetSchoolyearList()
    {
        var resource = "schoolyears";
        List<SchoolYearDto> Default = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<Model.Secretary.SchoolYearEntity> GetNewSchoolYears(Model.Secretary.SchoolYearEntity Default)
    {
        var resource = "schoolyears";
        return await _apiConsumer.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> CreateSchoolyear(Model.Secretary.SchoolYearEntity schoolYearEntity, List<PartialListDto> partials)
    {
        var resource = "schoolyears";
        Model.Secretary.SchoolYearEntity Default = new(); 
        var content = new CreateSchoolYearDto(schoolYearEntity, partials);
        var response = await _apiConsumer.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }
}