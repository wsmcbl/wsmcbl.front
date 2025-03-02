using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Controller;

public class ApplyArrearsController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public ApplyArrearsController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<TariffEntity>> GetTariffsOverdues()
    {
        List<TariffEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Accounting, "tariffs/overdues", defaultResult);
    }
    
    public async Task<bool> ActiveArrears(int tariffId)
    {
        List<TariffEntity> defaultResult = [];
        return await _apiConsumer.PutAsync(Modules.Accounting, $"tariffs/{tariffId}", defaultResult);
    }

    public async Task<List<DropDownItem>> GetTypeTariffList()
    {
        var resource = "tariffs/tariffs/types";
        List<TypeTariffDto> Default = [];
        var response = await _apiConsumer.GetAsync(Modules.Accounting, resource, Default);
        return response.Select(dto => dto.ToDropdownList()).ToList();
    }
}