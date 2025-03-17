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
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, $"{resource}/overdues", defaultResult);
    }
    
    public async Task<bool> ActiveArrears(int tariffId)
    {
        List<TariffEntity> defaultResult = [];
        return await apiFactory.WithNotificator.PutAsync(Modules.Accounting, $"{resource}/{tariffId}", defaultResult);
    }

    public async Task<List<DropDownItem>> GetTariffTypeList()
    {
        List<TariffTypeEntity> defaultResult = [];

        var uri = $"{resource}/types";
        var result = await apiFactory.WithNotificator.GetAsync(Modules.Accounting, uri, defaultResult);
        
        return result.Select(e => new DropDownItem(e)).ToList();
    }
}