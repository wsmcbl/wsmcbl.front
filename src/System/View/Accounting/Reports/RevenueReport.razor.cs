using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReport : ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    private bool HasData { get; set; } = true;
    
    private bool IsLoad()
    {
        return false;
    }
}