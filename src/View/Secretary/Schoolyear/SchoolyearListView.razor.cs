using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public partial class SchoolyearListView : BaseView
{
    private List<BasicSchoolyearDto>? SchoolyearList;
    
    [Inject] protected CreateSchoolyearController Controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolyearList = await Controller.GetSchoolYearList();
    }

    protected override bool IsLoading()
    {
        return SchoolyearList == null;
    }
}