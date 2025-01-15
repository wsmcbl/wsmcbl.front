using Microsoft.AspNetCore.Components;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class InitializeEnrollmentView : BaseView
{
    [Parameter] public string degreeId { get; set; } = null!;
    
    protected override bool IsLoading()
    {
        Task.Delay(2000);
        return false;
    }
}
