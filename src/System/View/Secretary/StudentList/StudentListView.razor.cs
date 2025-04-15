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
    private byte[] PdfDocument { get; set; } = [];
    private string? PdfDocumentName { get; set; }
    private string ThisStudent { get; set; } = string.Empty;
    
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
    private Task HandlePdfDocumentChanged(byte[] newPdf)
    {
        PdfDocument = newPdf;
        return Task.CompletedTask;
    }
    
    private async Task PrintReportCard(string studentId)
    {
        ThisStudent = studentId;
        PdfDocumentName = "Boleta de calificaciones";
        await Navigator.ShowModal("DowloadDegreeDocument");
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