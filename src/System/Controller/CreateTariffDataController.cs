using wsmcbl.src.Controller.Service;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Controller;

public class CreateTariffDataController : BaseController
{
    public CreateTariffDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs/tariffs")
    {
    }

    public async Task<bool> CreateNewTariff(SchoolyearTariffDto schoolyearTariff)
    {
        TariffDataDto Default = new();
        var content = MapperDate.MapToTariffDataDto(schoolyearTariff);
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, resource, content, Default);
        return response != Default;
    }

    public async Task<List<DropDownItem>> GetTypeTariffList()
    {
        List<TypeTariffDto> Default = [];
        var response = await apiFactory.Default.GetAsync(Modules.Accounting, $"{resource}/types", Default);
        return response.Select(dto => dto.ToDropdownList()).ToList();
    }
}