using Microsoft.AspNetCore.Components;
using wsmcbl.src.Service;

namespace wsmcbl.src.View.Shared;

public partial class ErrorView : ComponentBase
{
    [Inject] private ErrorService error { get; set; }
    private string? errorMessage;
    
    
    [Inject] private AlertService AlertService { get; set; }

    protected override void OnInitialized()
    {
        error.OnError += HandleError;
    }
    private void HandleError(string message)
    {   
        errorMessage = message;
    }

    public void Dispose()
    {
        error.OnError -= HandleError;
    }
}