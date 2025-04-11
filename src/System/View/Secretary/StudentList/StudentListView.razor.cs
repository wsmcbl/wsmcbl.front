using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class StudentListView : BaseView
{
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected EnrollStudentController EnrollController { get; set; } = null!;
    [Inject] protected UnenrollController unenrollController { get; set; } = null!;
    [Inject] protected UpdateStudentController UpdateController { get; set; } = null!;
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;

    private string EnrollmentNameForChange { get; set; } = string.Empty;
    private string StudentIdForMove { get; set; } = string.Empty;
    private string StudentIdForChangeEducationLevel { get; set; } = string.Empty;
    private byte[]? PdfDocument { get; set; }
    private string? PdfDocumentName { get; set; }
    
    //var for paginator
    private Paginator<StudentEntity>? studentList { get; set; }
    private PagedRequest Request { get; set; } = new();
    private bool hasData {get; set;}


    protected override void OnParametersSet()
    {
        PdfDocument = [];
    }
    protected override async Task OnInitializedAsync()
    {
        UpdateRequest();
        await LoadStudentList();
    }
    private async Task LoadStudentList()
    {
        studentList = await UpdateController.GetStudentsPaged(Request);
        hasData = studentList.data.Count > 0;
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
        await Navigator.ShowModal("MoveStudentModal");
    }
    private async Task UpdateEducationLevel(string studentId)
    {
        StudentIdForChangeEducationLevel = studentId;
        await Navigator.ShowModal("ChangeEducationLevelModal");
    }
    private async Task Withdraw(string studentId, string studentName)
    {
        var desc = await Notificator.ShowAlertQuestion("Advertencia", $"¿Estas seguro que deseas dar de baja al estudiante {studentName}?", ("Si","No"));
        if (desc)
        {
            var response = await unenrollController.Withdraw(studentId);
            if (response)
            {
                await Notificator.ShowSuccess($"Hemos dado de baja con exito al estudiante {studentName}");
                await LoadStudentList();
            }
        }
    }
    private async Task ChangeStudentState(string studentId, string fullName)
    {
        var desc = await Notificator.ShowAlertQuestion("Advertencia", $"¿Estas seguro que deseas cambiar es estado de: {fullName}?", ("Si","No"));
        if (desc)
        {
            var response = await UpdateController.UpdateStudentState(studentId);
            if (response)
            {
                await Notificator.ShowSuccess($"Hemos actualizado el estado del estudiante {fullName}");
                await LoadStudentList();
            }
        }
    }
    
    
    private string GetStatusLabel(bool value) => value ? "active-status" : "inactive-status";
    
    //Method for paginator
    private Task UpdateUrl()
    {
        var uri = $"/secretary/students{Request}";
        Navigator.UpdateUrl(uri);
        return Task.CompletedTask;
    }
    private void  UpdateRequest()
    {
        var uri = new Uri(Navigator.GetUrl());
        var queryParams = QueryHelpers.ParseQuery(uri.Query);

        if (queryParams.TryGetValue("search", out var search))
        {
            Request.SearchText = search;
        }

        if (queryParams.TryGetValue("sortBy", out var sortBy))
        {
            Request.sortBy = sortBy;
        }

        if (queryParams.TryGetValue("isAscending", out var isAscending))
        {
            Request.isAscending = bool.Parse(isAscending!);
        }

        if (queryParams.TryGetValue("page", out var page))
        {
            Request.CurrentPage = int.Parse(page!);
        }

        if (queryParams.TryGetValue("pageSize", out var pageSize))
        {
            Request.pageSize = int.Parse(pageSize!);
        }

        if (queryParams.TryGetValue("quantity", out var quantity))
        {
            Request.Quantity = int.Parse(quantity!);
        }
    }
    private async Task SortByColumn(string columnName)
    {
        if (Request.sortBy == columnName)
        {
            Request.isAscending = !Request.isAscending;
        }
        else
        {
            Request.sortBy = columnName;
            Request.isAscending = true;
        }

        Request.sortBy = columnName;
        await UpdateUrl();
        await LoadStudentList();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await UpdateUrl();
            await LoadStudentList();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= studentList!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await UpdateUrl();
            await LoadStudentList();
        }
    }
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            if (Request.CurrentPage > 1)
            {
                Request.CurrentPage = 1;
            }
            await UpdateUrl();
            await LoadStudentList();
            if (studentList != null) hasData = studentList.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await UpdateUrl();
        await LoadStudentList();
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
}