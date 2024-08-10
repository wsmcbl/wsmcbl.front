using Microsoft.AspNetCore.Components;
using wsmcbl.src.Service;

namespace wsmcbl.src.View.Shared;

public partial class ErrorHandler : ComponentBase
{
    private bool HasError { get; set; }
    private string? ErrorMessage { get; set; }

    [Inject]
    private ErrorService ErrorService { get; set; }

    protected override void OnInitialized()
    {
        ErrorService.OnError += HandleError;
    }

    private void HandleError(string message)
    {
        HasError = true;
        ErrorMessage = message;
        StateHasChanged();
    }
}