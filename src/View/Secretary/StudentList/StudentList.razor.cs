using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.StudentList;

public class StudentList_view : BaseView
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; }
    protected ICollection<StudentEntity>? StudentList;
    protected byte[] pdfContent;
    
    protected override async Task OnInitializedAsync()
    {
        StudentList = await Controller.GetStudentList();
    }

    protected override bool IsLoad()
    {
        return StudentList == null;
    }
    
    protected async Task PrintSheetCalification(string studenId)
    {
        pdfContent = await PrintController.GetPdfContent(studenId);
        if (pdfContent == null || pdfContent.Length == 0)
        {
            return;
        }
        await Navigator.InvokeModal("eval", "$('#ModalCalificationPdf').modal('show');");
    }
}