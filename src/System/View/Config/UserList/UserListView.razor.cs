using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Config.UserList;

public partial class UserListView : BaseView
{
    [Inject] private CreateUserController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private string? UserIdForViewInformation {get; set;}
    private UserEntity? User { get; set; }
    private PagedRequest Request { get; set; } = new();
    private Paginator<UserToListDto>? UserPaginator { get; set; }
    private bool hasData {get; set;}
    
    protected override async Task OnParametersSetAsync()
    {
        await LoadUserList();
    }
    private async Task LoadUserList()
    {
        UserPaginator = await Controller.GetUserList(Request);
        hasData = UserPaginator.data.Count > 0;
    }
    private async Task HandleUserUpdated(UserEntity updatedUser)
    {
        User = updatedUser;
        await LoadUserList();
    }
    private async Task ViewUserData(string userId)
    {
        UserIdForViewInformation = userId;
        await Navigator.ShowModal("FullUserInfoModal");
        StateHasChanged();
    }
    protected override bool IsLoading()
    {
        return UserPaginator == null;
    }
    
    private async Task ChangePassword(string itemUserId)
    {
        var response = await Notificator.ShowAlertQuestion("Alerta","¿Estás seguro que deseas cambiar la contraseña?",("Si","No"));
        if (response)
        {
            User = await Controller.ChangePassword(itemUserId);
            await Navigator.ShowModal("InfoUserModal");
            StateHasChanged();
        }
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
        await LoadUserList();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await LoadUserList();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= UserPaginator!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await LoadUserList();
        }
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            if (Request.CurrentPage > 1)
            {
                Request.CurrentPage = 1;
            }
            await LoadUserList();
            if (UserPaginator != null) hasData = UserPaginator.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await LoadUserList();
    }
}