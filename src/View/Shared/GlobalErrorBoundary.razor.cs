using Microsoft.AspNetCore.Components;
using wsmcbl.src.Service;

namespace wsmcbl.src.View.Shared;

public partial class GlobalErrorBoundary : ComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    private ErrorService ErrorService { get; set; }
   
    private void HandleError(Exception exception)
    {
        ErrorService.ShowError("Ha ocurrido un error inesperado. Por favor, inténtelo de nuevo más tarde.");
    }
}