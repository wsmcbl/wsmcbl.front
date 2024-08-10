using Microsoft.AspNetCore.Components;
using wsmcbl.src.Service;

namespace wsmcbl.src.View.Shared;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private ErrorService ErrorService { get; set; }
    private string? errorMessage;

    protected override void OnInitialized()
    {
        ErrorService.OnError += HandleError;
    }

    private void HandleError(string message)
    {
        errorMessage = message;
        StateHasChanged();
    }
}