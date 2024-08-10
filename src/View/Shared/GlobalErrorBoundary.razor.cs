using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Service;

namespace wsmcbl.src.View.Shared;

public partial class GlobalErrorBoundary : ComponentBase
{
    private ErrorBoundary? errorBoundary;
    [Parameter] public RenderFragment ChildContent { get; set; }
}