using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

namespace wsmcbl.src.Controller;

public class CreateTariffDataController : BaseController
{
    public CreateTariffDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs/tariffs")
    {
    }
    
    public async Task<CreateTariffDto?> CreateTariffData(CreateTariffDto tariff)
    {
        return await apiFactory.WithNotificator.PostAsync<CreateTariffDto, CreateTariffDto?>(Modules.Secretary, path, tariff, null);
    }
    
    public async Task<bool> UpdateTariffData(CreateTariffDto tariff)
    {
        var resource = $"{path}/{tariff.tariffDataId}";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, tariff);
    }
    
    public async Task<List<DropDownItem>> GetTariffTypeList()
    {
        var controller = new ApplyArrearsController(apiFactory);
        return await controller.GetTariffTypeList();
    }
    
}