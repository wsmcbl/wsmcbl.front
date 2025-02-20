using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting;

public partial class StudentListView : BaseView
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    private Paginator<StudentEntity>? StudentList { get; set; }
    private bool hasData {get; set;}

    private string SearchText { get; set; } = string.Empty;
    private string SortColumn { get; set; } = string.Empty;
    private int CurrentPage { get; set; } = 1;
    private int PageSize { get; set; } = 10;
    private bool IsAscending = true;  
    
    protected override async Task OnInitializedAsync()
    {
        await loadStudentList();
    }
    
    private async Task loadStudentList()
    {
        StudentList = await Controller.GetStudentList(SearchText, SortColumn, CurrentPage, PageSize, IsAscending);
        hasData = StudentList.data.Count > 0;
    }

    protected override bool IsLoading()
    {
        return StudentList == null;
    }
    
    private async Task SortByColumn(string columnName)
    {
        if (SortColumn == columnName)
        {
            IsAscending = !IsAscending;
        }
        else
        {
            SortColumn = columnName;
            IsAscending = true;
        }

        SortColumn = columnName;
        await loadStudentList();
    }
    
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            PageSize = selectedValue;
            CurrentPage = 1;
            await loadStudentList();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= StudentList!.totalPages)
        {
            CurrentPage = pageNumber;
            await loadStudentList();
        }
    }
    
    private async Task GoToPreviousPage() => await ShowPage(CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(CurrentPage + 1);

    
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await loadStudentList();
            if (StudentList != null) hasData = StudentList.data.Count > 0;
        }
    }

    private async Task ClearSearch()
    {
        SearchText = string.Empty;
        await loadStudentList();
    }
}