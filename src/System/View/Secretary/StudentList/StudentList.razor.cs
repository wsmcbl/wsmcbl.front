using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class StudentList : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; }
    protected ICollection<StudentEntity>? List { get; set; }

    protected override void OnParametersSet()
    {
        ReportCardPdf = [];
    }

    protected override async Task OnInitializedAsync()
    {
        List = await Controller.GetStudentList();
    }

    protected bool IsLoad()
    {
        return List == null;
    }
    
    private byte[] ReportCardPdf { get; set; }

    private async Task PrintReportCard(string studentId)
    {
        ReportCardPdf = await PrintController.GetPdfContent(studentId);
        
        if(ReportCardPdf.Length == 0)
        {
            return;
        }

        await Navigator.ShowPdfModal();
    }
}
