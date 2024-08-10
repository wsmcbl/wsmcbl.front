using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace wsmcbl.src.View.Shared;

public class GlobalError : ComponentBase
{
    protected ErrorBoundary? ErrorBoundary { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}