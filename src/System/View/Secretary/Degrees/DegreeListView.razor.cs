using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class DegreeListView : BaseView
{
    [Parameter] public string? degreeId { get; set; }
    [Parameter] public int SectionsNumber { get; set; }
    
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateEnrollmentController controller { get; set; } = null!;
    [Inject] protected CreateSchoolYearController SchoolYearController { get; set; } = null!;
    [Inject] protected PrintDocumentController documentController { get; set; } = null!;
    
    private DegreeEntity? Degree { get; set; }
    private List<SchoolYearDto>? ThisSchoolYear; 
    
    //Var for paginator
    private Paginator<DegreeEntity>? DegreeList { get; set; }
    private PagedRequest Request { get; set; } = new() { pageSize = 25 };
    private bool hasData {get; set;}
    
    
    protected override async Task OnInitializedAsync()
    {
        await Load();
        ThisSchoolYear = await SchoolYearController.GetSchoolYearsList();
    }
    private async Task Load()
    {
        DegreeList = await controller.GetDegreeList(Request);
        hasData = DegreeList.data.Count > 0;
    }
    private async Task CreateEnrollmentModal(string value)
    {
        degreeId = value;
        await Navigator.ShowModal("confGrade");
    }
    private void ViewGrade(string value)
    {
        degreeId = value;
        Navigator.ToPage($"secretary/degrees/{degreeId}/enrollments");
    }
    private async Task CreateEnrollments(string value, int quantity)
    {
        if (quantity is < 1 or >= 7)
        {
            await Notificator.ShowInformation("El número máximo de secciones es 7.");
            return;
        }

        degreeId = value;
        Degree = await controller.CreateEnrollments(value, quantity);
        if (Degree == null)
        {
            await Notificator.ShowError("Hubo un fallo al crear las secciones.");
            return;
        }

        await Navigator.HideModal("confGrade");
        await Load();
        StateHasChanged();
        await Navigator.ShowModal("InitGrade");
    }
    protected override bool IsLoading()
    {
        return DegreeList == null;
    }
    private byte[]? pdf { get; set; }
    private async Task GetOfficialEnrollmentListDocument()
    {
        pdf = await documentController.GetOfficialEnrollmentListDocument();
        if (pdf.Length == 0)
        {
            await Notificator.ShowError("No se pudo generar el documento.");
            return;
        }
        
        await Navigator.ShowPdfModal();
    }
    
    
    //Method for paginator
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
        await Load();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await Load();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= DegreeList!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await Load();
        }
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await Load();
            if (DegreeList != null) hasData = DegreeList.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await Load();
    }
}
