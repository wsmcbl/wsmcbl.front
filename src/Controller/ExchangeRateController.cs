using System.Globalization;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.ExchangeRates;

namespace wsmcbl.src.Controller;

public class ExchangeRateController : BaseController
{
    public ExchangeRateController(ApiConsumerFactory apiFactory) : base(apiFactory, "schoolyears")
    {
    }
    
    public async Task<List<ExchangeRateDto>> GetExchangeRateList()
    {
        List<ExchangeRateDto> defaultResult = new ();
        var resource = $"{path}/rates";
        return await apiFactory.WithNotificator.GetAsync(Modules.Secretary, resource, defaultResult);
    }

    public async Task<bool> UpdateExchangeRate(decimal value)
    {
        var valueString = value.ToString(CultureInfo.InvariantCulture);
        var resource = $"{path}/rates/current?exchange={valueString}";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, false);
    }
}