using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class ForgetDebtController : BaseController
{
    public ForgetDebtController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<List<DebtDto>> GetDebtList(string studentId)
    {
        var resource = $"{path}/{studentId}/debts";
        var defaultResult = new List<DebtDto>();
        await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);
        
        return defaultResult;
    }
    
    public async Task<bool> ForgetDebt(string studentId, int tariffId, string authorizationToken)
    {
        var resource = $"{path}/{studentId}/debts?tariffId={tariffId}&authorizationToken={authorizationToken}";
        return await apiFactory.WithNotificator.PutAsync<object>(Modules.Accounting, resource, null);
    }
}