using System.Text.Json;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.Details;
using wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.Controller;

public class CreateSchoolyearController : BaseController
{
    public CreateSchoolyearController(ApiConsumerFactory apiFactory) : base(apiFactory, "schoolyears")
    {
    }
    
    public async Task<List<BasicSchoolyearDto>> GetSchoolYearList()
    {
        return await apiFactory.Default
            .GetAsync(Modules.Secretary, path, new List<BasicSchoolyearDto>());
    }
    
    public async Task<SchoolYearFullDto> GetSchoolYearById(string schoolYearId)
    {
        var resource = $"{path}/{schoolYearId}";
        var defaultResult = new SchoolYearFullDto();
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    
    public async Task<bool> CreateSchoolYear(CreateSchoolYearDto value)
    {
        var defaultResult = new CreateSchoolYearDto();
        var json = JsonSerializer.Serialize(value);
        Console.WriteLine(json);
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, path, value, defaultResult);
        return response != defaultResult;
    }
    
    public async Task<List<ExchangeRateEntity>> GetExchangeRateList()
    {
        var resource = $"{path}/rates";
        return await apiFactory.Default
            .GetAsync(Modules.Secretary, resource, new List<ExchangeRateEntity>());
    }

    public async Task<List<CreateTariffDto>> GetTariffList()
    {
        var resource = "/catalogs/tariffs";
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, new List<CreateTariffDto>());
    }
}