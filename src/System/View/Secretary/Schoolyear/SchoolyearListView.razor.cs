using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public partial class SchoolyearListView : BaseView
{
    private List<BasicSchoolyearDto>? SchoolyearList;
    
    [Inject] protected CreateSchoolyearController Controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolyearList = await Controller.GetSchoolyearList();
    }

    protected override bool IsLoading()
    {
        return SchoolyearList == null;
    }
}