using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Schoolyear.TariffData;

namespace wsmcbl.src.Controller;

public class CreateTariffDataController : BaseController
{
    public CreateTariffDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs/tariffs")
    {
    }

    public async Task<List<TariffDataDto>> GetTariffDataList()
    {
        var Default = new List<TariffDataDto>();
        return await apiFactory.Default.GetAsync(Modules.Secretary, path, Default);
    }

    public async Task<TariffDataDto> CreateTariffData(TariffDataDto value)
    {
        TariffDataDto Default = new();
        return await apiFactory.Default.PostAsync(Modules.Secretary, path, value, Default);
    }

    public async Task<bool> UpdateTariffData(TariffDataDto value)
    {
        var resource = $"{path}/{value.tariffDataId}";
        return await apiFactory.Default.PutAsync(Modules.Secretary, resource, value);
    }

    public async Task<List<DropDownItem>> GetTariffTypeList()
    {
        var controller = new ApplyArrearsController(apiFactory);

        return await controller.GetTariffTypeList();
    }
}