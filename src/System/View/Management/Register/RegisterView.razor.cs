using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Management.Register;

public partial class RegisterView : BaseView
{
    [Inject] GenerateStudentRegisterController Controller { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;

    private RegisterDto ThisStudent = new();
    
    //var for paginator
    private Paginator<RegisterDto>? Padron { get; set; }
    private PagedRequest Request { get; set; } = new();
    private bool hasData {get; set;}

    protected override async Task OnParametersSetAsync()
    {
        UpdateRequest();
        await LoadPadron();
    }
    private async Task LoadPadron()
    {
        Padron = await Controller.GetPadronList(Request);
        if (Padron != null) hasData = Padron.data.Count > 0;
    }
    private async Task DowloadPadron()
    {
        await Controller.DownloadPadron();
    }
    protected override bool IsLoading()
    {
        return Padron == null;
    }

    private async Task ShowInfoStudent(RegisterDto item)
    {
        ThisStudent = item;
        await Navigator.ShowModal("PdfStudentInfoModal");
    }
    private string GetStatusLabel(bool value) => value ? "active-status" : "inactive-status";
    
    //Method for paginator
    private Task UpdateUrl()
    {
        var uri = $"/management/registers{Request}";
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
        await LoadPadron();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await UpdateUrl();
            await LoadPadron();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= Padron!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await UpdateUrl();
            await LoadPadron();
        }
    }
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await UpdateUrl();
            await LoadPadron();
            if (Padron != null) hasData = Padron.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await UpdateUrl();
        await LoadPadron();
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    
}