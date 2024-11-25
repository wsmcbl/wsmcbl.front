using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReport : ComponentBase
{
    private bool HasData { get; set; } = true;
    private bool IsLoad()
    {
        return false;
    }
}