using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class ForgetDebtController : BaseController
{
    public ForgetDebtController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<Paginator<DebtDto>> GetDebtList(string studentId, PagedRequest request)
    {
        var resource = $"{path}/{studentId}/debts{request}";
        var defaultResult = new Paginator<DebtDto>();
        var a = await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);
        
        return a;
    }
    
    public async Task<bool> ForgetDebt(string studentId, int tariffId, string authorizationToken)
    {
        var resource = $"{path}/{studentId}/debts?tariffId={tariffId}&authorizationToken={authorizationToken}";
        return await apiFactory.WithNotificator.PutAsync<object>(Modules.Accounting, resource, null);
    }
}