using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class StudentList : ComponentBase
{
    [Inject] protected EnrollStudentController EnrollController { get; set; } = null!;
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] protected Navigator? Navigator { get; set; }
    protected ICollection<StudentEntity>? List { get; set; }
    private string EnrollmentNameForChange { get; set; } = string.Empty;
    private string StudentIdForMove { get; set; } = string.Empty;
    private byte[]? PdfDocument { get; set; }
    private string? PdfDocumentName { get; set; }


    protected override void OnParametersSet()
    {
        ReportCardPdf = [];
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadStudentList();
    }

    private async Task LoadStudentList()
    {
        List = await PrintController.GetAllStudentsList();
    }
    
    protected bool IsLoad()
    {
        return List == null;
    }

    private void ToProfileUpdate(string studentId)
    {
        Navigator!.ToPage($"/secretary/update-profile-picture/{studentId}");
    }
    
    private byte[]? ReportCardPdf { get; set; }
    private async Task PrintReportCard(string studentId)
    {
        ReportCardPdf = await PrintController.GetPdfContent(studentId);
        PdfDocument = ReportCardPdf;
        PdfDocumentName = "Boleta de calificaciónes";
        
        if(ReportCardPdf.Length == 0)
        {
            return;
        }

        await Navigator!.ShowPdfModal();
    }
    
    private byte[]? EnrollSheetPdf { get; set; }
    private async Task PrintEnrollSheet(string studentId)
    {
        EnrollSheetPdf = await EnrollController.GetEnrollSheetPdf(studentId);
        PdfDocument = EnrollSheetPdf;
        PdfDocumentName = "Hoja de matrícula";
        
        if (EnrollSheetPdf.Length == 0)
        {
            return;
        }

        await Navigator!.ShowPdfModal();
    }

    private async Task UpdateEnrollment(string studentId, string enrollmentId)
    {
        StudentIdForMove = studentId;
        EnrollmentNameForChange = enrollmentId;
        await Navigator!.ShowModal("MoveStudentModal");
    }
}
