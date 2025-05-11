using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting;

namespace wsmcbl.src.Controller;

public class UpdateStudentAccountingController : BaseController
{
    public UpdateStudentAccountingController(ApiConsumerFactory apiConsumerFactory) : base(apiConsumerFactory, "students")
    {
    }
    
    public async Task<bool> UpdateDiscount(UpdateDiscountDto updateDiscountDto)
    {
        return await apiFactory.WithNotificator.PutAsync(Modules.Accounting, path, updateDiscountDto);
    }
}