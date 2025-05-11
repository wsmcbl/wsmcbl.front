using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class ApplyArrearsController : BaseController
{
    public ApplyArrearsController(ApiConsumerFactory apiConsumerFactory) : base(apiConsumerFactory, "tariffs")
    {
    }
    
    public async Task<List<TariffEntity>> GetTariffOverdueList()
    {
        List<TariffEntity> defaultResult = [];
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, $"{path}/overdues", defaultResult);
    }
    
    public async Task<bool> ActiveArrears(int tariffId)
    {
        List<TariffEntity> defaultResult = [];
        return await apiFactory.WithNotificator.PutAsync(Modules.Accounting, $"{path}/{tariffId}", defaultResult);
    }

    public async Task<List<DropDownItem>> GetTariffTypeList()
    {
        List<TariffTypeEntity> defaultResult = [];

        var resource = $"{path}/types";
        var result = await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);
        
        return result.Select(e => new DropDownItem(e)).ToList();
    }
}