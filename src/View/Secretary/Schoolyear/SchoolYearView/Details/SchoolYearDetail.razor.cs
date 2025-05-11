using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.Details;

public partial class SchoolYearDetail : BaseView
{
    [Parameter] public string schoolYearId { get; set; } = string.Empty;
    [Inject] CreateSchoolyearController Controller { get; set; } = null!;
    private SchoolYearFullDto? SchoolYear { get; set; }
    
    
    private int selectedEducationalLevel = 0;
    private IEnumerable<TariffEntity> FilteredTariffs => 
        selectedEducationalLevel == 0 
            ? SchoolYear!.tariffList 
            : SchoolYear!.tariffList.Where(t => t.educationalLevel == selectedEducationalLevel);

    private string GetEducationalLevelName(int level)
    {
        return level switch
        {
            1 => "Preescolar",
            2 => "Primaria",
            3 => "Secundaria",
            _ => "Desconocido"
        };
    }

    protected override async Task OnParametersSetAsync()
    {
        SchoolYear = await Controller.GetSchoolyearById(schoolYearId);
    }
    protected override bool IsLoading()
    {
        return SchoolYear == null;
    }
}