using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Secretary.Schoolyear;
using wsmcbl.src.View.Secretary.Schoolyear.TariffData;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.Controller;

public class CreateSchoolyearController : BaseController
{
    public CreateSchoolyearController(ApiConsumerFactory apiFactory) : base(apiFactory, "schoolyears")
    {
    }
    
    public async Task<List<BasicSchoolyearDto>> GetSchoolyearList()
    {
        return await apiFactory.Default
            .GetAsync(Modules.Secretary, path, new List<BasicSchoolyearDto>());
    }
    
    public async Task<SchoolyearDto> GetSchoolyearById(string schoolyearId)
    {
        var resource = $"{path}/{schoolyearId}";
        
        var Default = new SchoolyearDto();
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> CreateSchoolyear(SchoolyearToCreateDto value)
    {
        var Default = new SchoolyearDto();
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, path, value, Default);

        return response != Default;
    }
    
    public async Task<List<ExchangeRateEntity>> GetExchangeRateList()
    {
        var resource = $"{path}/rates";
        return await apiFactory.Default
            .GetAsync(Modules.Secretary, resource, new List<ExchangeRateEntity>());
    }

    public async Task<List<TariffDataDto>> GetTariffList()
    {
        var resource = "/catalogs/tariffs";
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, new List<TariffDataDto>());
    }
}