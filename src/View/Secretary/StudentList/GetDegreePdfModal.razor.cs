using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class GetDegreePdfModal : ComponentBase
{
    [Inject] protected PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Parameter] public byte[] PdfDocument { get; set; } = [];
    [Parameter] public EventCallback<byte[]> PdfDocumentChanged { get; set; }
    [Parameter] public string StudentId {get;set;} = string.Empty;
    private string Token {get;set;} = string.Empty;
    private string AlertMessage {get;set;} = string.Empty;
    
    protected override async Task OnParametersSetAsync()
    {
        await CheckSolvency();
    }

    private async Task CheckSolvency()
    {
        if (!StudentId.IsNullOrEmpty())
        {
            try
            {
                PdfDocument = await PrintController.GetDegree(StudentId);
                if (PdfDocument.Length > 0){AlertMessage = "El estudiante esta solvente.";}
            }
            catch (Exception e)
            {
                AlertMessage = "Advertencia el estudiante no esta solvente.";
                Console.WriteLine(e);
            }
        }
    }
    private async Task HandleKeyDown(KeyboardEventArgs key)
    {
        if (key is { Key: "Enter"})
        {
            await DownloadDocument();
        }
    }
    private async Task HandleSubmit()
    {
        if (string.IsNullOrEmpty(Token))
        {
            return;
        }
        await DownloadDocument();
    }
    private async Task DownloadDocument()
    {
        if (Token != string.Empty)
        {
            PdfDocument = await PrintController.GetDegreeWhitToken(StudentId, Token);
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