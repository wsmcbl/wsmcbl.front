using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class StudentListView : BaseView
{
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected EnrollStudentController EnrollController { get; set; } = null!;
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;

    private ICollection<StudentEntity>? studentList { get; set; }
    private string EnrollmentNameForChange { get; set; } = string.Empty;
    private string StudentIdForMove { get; set; } = string.Empty;
    private string StudentIdForChangeEducationLevel { get; set; } = string.Empty;

    private byte[]? PdfDocument { get; set; }
    private string? PdfDocumentName { get; set; }


    protected override void OnParametersSet()
    {
        PdfDocument = [];
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadStudentList();
    }

    private async Task LoadStudentList()
    {
        studentList = await PrintController.GetAllStudentsList();
    }

    protected override bool IsLoading()
    {
        return studentList == null;
    }

    private async Task PrintReportCard(string studentId)
    {
        PdfDocument = await PrintController.GetPdfContent(studentId);
        if (PdfDocument.Length == 0)
        {
            return;
        }

        PdfDocumentName = "Boleta de calificaciones";
        await Navigator.ShowPdfModal();
    }
    
    private async Task PrintEnrollSheet(string studentId)
    {
        PdfDocument = await EnrollController.GetEnrollSheetPdf(studentId);
        if (PdfDocument.Length == 0)
        {
            return;
        }

        PdfDocumentName = "Hoja de matrícula";
        await Navigator.ShowPdfModal();
    }

    private async Task UpdateEnrollment(string studentId, string enrollmentId)
    {
        StudentIdForMove = studentId;
        EnrollmentNameForChange = enrollmentId;
        await Navigator!.ShowModal("MoveStudentModal");
    }
    
    private async Task UpdateEducationLevel(string studentId)
    {
        StudentIdForChangeEducationLevel = studentId;
        await Navigator!.ShowModal("ChangeEducationLevelModal");
    }

    private string getStatusLabel(bool value) => value ? "active-status" : "inactive-status";
}