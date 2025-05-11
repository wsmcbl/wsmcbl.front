using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using wsmcbl.src.View.Secretary.Schoolyear;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.View.Components.GenerateProforma;

public partial class GeneratePriceView : ComponentBase
{
    [Inject] protected PrintDocumentByStudentController printDocumentByStudentController { get; set; } = null!;
    [Inject] protected CreateSchoolyearController SchoolyearController { get; set; } = null!;
    [Inject] protected CreateEnrollmentController controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private List<BasicSchoolyearDto>? _schoolyearList;
    private PagedRequest Request { get; set; } = new() { pageSize = 100 };
    private Paginator<DegreeEntity>? DegreeList { get; set; }
    private List<DegreeEntity> DegreeActive { get; set; } = [];
    private string Name {get; set;} = string.Empty;
    private string DegreeId {get; set;} = string.Empty;
    
    protected override async Task OnParametersSetAsync()
    {
        DegreeList = await controller.GetDegreeList(Request);
        _schoolyearList = await SchoolyearController.GetSchoolyearList();
        await GetDegreeActive();
    }

    private async Task GetDegreeActive()
    {
        if (_schoolyearList != null)
        {
            var activeSchoolYear = _schoolyearList.FirstOrDefault(s => s.isActive);
            
            if (DegreeList != null)
                DegreeActive = DegreeList.data
                    .Where(t => t.schoolYear == activeSchoolYear!.schoolyearId)
                    .ToList();
        }
        else
        {
            await Notificator.ShowError(
                "Obtubimos problemas al cargar la los años lectivos y los grados. (Modulo Generar Proformas)");
        }
    }

    private async Task DownloadDocument()
    {
        await printDocumentByStudentController.GetProforma(DegreeId, Name);
    }
    private void GetSelectDegreeId(ChangeEventArgs e)
    {
       DegreeId = e.Value!.ToString()!;
    }
}