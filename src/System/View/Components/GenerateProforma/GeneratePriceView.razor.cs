using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Components.GenerateProforma;

public partial class GeneratePriceView : ComponentBase
{
    [Inject] protected MoveStudentFromEnrollmentController Controller { get; set; } = null!;
    [Inject] protected PrintDocumentByStudentController printDocumentByStudentController { get; set; } = null!;
    [Parameter] public string studentId { get; set; } = string.Empty;
    private List<DegreeBasicDto>? Degrees { get; set; }
    private string Name {get; set;} = string.Empty;
    private string DegreeId {get; set;} = string.Empty;
    
    protected override async Task OnParametersSetAsync()
    {
        Degrees = await Controller.GetDegreeBasicList(studentId);
    }

    private async Task DownloadDocument()
    {
        await printDocumentByStudentController.GetProforma(studentId);
    }
    private void GetSelectDegreeId(ChangeEventArgs e)
    {
       DegreeId = e.Value!.ToString()!;
    }
}