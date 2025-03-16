using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Controller;

public class CreateTariffDataController
{
    private readonly ApiConsumerFactory apiFactory;

    public CreateTariffDataController(ApiConsumerFactory apiFactory)
    {
        this.apiFactory = apiFactory;
    }
    
    public async Task<bool> CreateNewTariff(SchoolyearTariffDto schoolyearTariff)
    {
        var resource = "schoolyears/tariffs";
        TariffDataDto Default = new();
        var content = MapperDate.MapToTariffDataDto(schoolyearTariff);
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }
    
    public async Task<List<DropDownItem>> GetTypeTariffList()
    {
        var resource = "tariffs/types";
        List<TypeTariffDto> Default = [];
        var response = await apiFactory.Default.GetAsync(Modules.Accounting, resource, Default);
        return response.Select(dto => dto.ToDropdownList()).ToList();
    }
}