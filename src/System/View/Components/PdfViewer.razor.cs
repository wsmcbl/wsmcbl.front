using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View.Components;

public partial class PdfViewer : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = null!;
    
    [Parameter]
    public byte[]? PdfContent { get; set; } 
    
}