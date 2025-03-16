using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class DegreeListView : BaseView
{
    [Parameter] public string? degreeId { get; set; }
    [Parameter] public int SectionsNumber { get; set; }
    
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateEnrollmentController controller { get; set; } = null!;
    [Inject] protected CreateSchoolyearController schoolyearController { get; set; } = null!;
    [Inject] protected PrintDocumentController documentController { get; set; } = null!;
    
    private DegreeEntity? Degree { get; set; }
    private List<BasicSchoolyearDto>? SchoolyearList; 
    
    //Var for paginator
    private Paginator<DegreeEntity>? DegreeList { get; set; }
    private PagedRequest Request { get; set; } = new() { pageSize = 25 };
    private bool hasData {get; set;}
    
    
    protected override async Task OnInitializedAsync()
    {
        await Load();
        SchoolyearList = await schoolyearController.GetSchoolyearList();
    }

    protected override void OnParametersSet()
    {
        UpdateRequest();
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
    private Task UpdateUrl()
    {
        var uri = $"/secretary/degrees{Request.ToString()}";
        Navigator.UpdateUrl(uri);
        return Task.CompletedTask;
    }
    private void UpdateRequest()
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
        await Load();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await UpdateUrl();
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
            await UpdateUrl();
            await Load();
        }
    }
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await UpdateUrl();
            await Load();
            if (DegreeList != null) hasData = DegreeList.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await UpdateUrl();
        await Load();
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
}
