using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class GetDegreePdfModal : ComponentBase
{
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Parameter] public byte[] PdfDocument { get; set; } = [];
    [Parameter]
    public EventCallback<byte[]> PdfDocumentChanged { get; set; }
    [Parameter] public string StudentId {get;set;} = string.Empty;
    private string Token {get;set;} = string.Empty;
    private async Task HandleKeyDown(KeyboardEventArgs key)
    {
        if (key is { Key: "Enter"})
        {
            await DowloadDocument();
        }
    }
    private async Task HandleSubmit()
    {
        if (string.IsNullOrEmpty(Token))
        {
            return;
        }
        await DowloadDocument();
    }
    private async Task DowloadDocument()
    {
        if (Token != string.Empty)
        {
            PdfDocument = await PrintController.GetPdfContent(StudentId, Token);
            if (PdfDocument.Length == 0)
            {
                return;
            }
            await PdfDocumentChanged.InvokeAsync(PdfDocument);
            await Navigator.HideModal("DowloadDegreeDocument");
            await Navigator.ShowPdfModal();
        }
    }
}