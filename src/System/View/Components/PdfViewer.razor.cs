using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace wsmcbl.src.View.Components;

public partial class PdfViewer : ComponentBase
{
    [Parameter] public string Title { get; set; } = null!;
    [Parameter] public byte[]? PdfContent { get; set; }
    [Inject] private IJSRuntime Js { get; set; }
    
    private bool hasPdf() =>  PdfContent != null && PdfContent.Length != 0;
    
    private async Task PrintPdf()
    {
        if (PdfContent != null)
        {
            var pdfBase64 = Convert.ToBase64String(PdfContent);
            await Js.InvokeVoidAsync("printPdf", pdfBase64);
        }
    }
}