using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.StudentPasswordView;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class GetDegreePdfModal : ComponentBase
{
    [Inject] private PrintReportCardStudentController PrintController { get; set; } = null!;
    [Inject] private UpdateStudentController UpdateStudentController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Parameter] public byte[] PdfDocument { get; set; } = [];
    [Parameter] public EventCallback<byte[]> PdfDocumentChanged { get; set; }
    [Parameter] public string StudentId {get;set;} = string.Empty;
    private string Token {get;set;} = string.Empty;
    private string AlertMessage {get;set;} = string.Empty;
    
    private StudentPassDto _studentBasic = new();
    private StudentDetailsDto _studentFull = new();


    protected override Task OnParametersSetAsync()
    {
        AlertMessage = "";
        Token = "";
        return Task.CompletedTask;
    }

    private async Task CheckSolvency()
    {
        if (string.IsNullOrEmpty(StudentId))
        {
            return;
        }

        var result = await CheckStudentStatus();
        if (result)
        {
            _studentFull = await PrintController.GetFullInfoStudent(_studentBasic.studentId, _studentBasic.accessToken); //obtenemos la informacion sobre la solvencia.

            if (_studentFull.hasSolvency)
            {
                PdfDocument = await PrintController.GetDegreeWhitStudentToken(_studentBasic.studentId, _studentBasic.accessToken);
            
                if (PdfDocument.Length == 0)
                {
                    return;
                }
                await PdfDocumentChanged.InvokeAsync(PdfDocument);
                await Navigator.HideModal("DowloadDegreeDocument");
                AlertMessage = "";
                await Navigator.ShowPdfModal();
            }
            else
            {
                AlertMessage = "Advertencia el estudiante no esta solvente.";
            } 
        }
    }

    private async Task<bool> CheckStudentStatus()
    {
        _studentBasic = await UpdateStudentController.GetStudentToken(StudentId); //obtuve el token del estudiante
        
        if (!_studentBasic.isActive || _studentBasic.accessToken == "N/A")
        {
            await Notificator.ShowInformation("El estudiante no esta activo o no tiene una contraseña asignada.");
            AlertMessage = "Advertencia el estudiante no esta activo.";
            return false;
        }
        
        return true;
    }
    
    
    private async Task DownloadDocument()
    {
        if (Token != string.Empty)
        {
            PdfDocument = await PrintController.GetDegreeWhitAdminToken(StudentId, Token);
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