using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.DashbardStudent;

public partial class DashboardStudentView : ComponentBase
{
    [Inject] protected PrintDocumentByStudentController PrintDocumentByStudentController { get; set; } = null!;
    [Inject] protected UpdateStudentController UpdateController { get; set; } = null!;
    [Inject] protected UnenrollController unenrollController { get; set; } = null!;
    [Inject] protected CollectTariffController collectTariffController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Parameter] public StudentEntity Student { get; set; } = new();
    private byte[] PdfDocument { get; set; } = [];


    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(Student.studentId))
        {
            return;
        }
        await LoadStudent();
    }
    private async Task LoadStudent()
    {
        Student = await UpdateController.GetStudentById(Student.studentId);
        StateHasChanged();
    }
    private string GetProfilePicture()
    {
        if (!string.IsNullOrEmpty(Student.profilePicture))
        {
            return $"data:image/png;base64,{Student.profilePicture}";
        }

        return "/img/Placeholder/Man.png";
    }
    private async Task UpdateEducationLevel()
    {
        await Navigator.ShowModal("ChangeEducationLevelModal");
    }
    private async Task Withdraw()
    {
        var desc = await Notificator.ShowAlertQuestion("Advertencia", $"¿Estas seguro que deseas dar de baja al estudiante {Student.FullName()}?", ("Si","No"));
        if (desc)
        {
            var response = await unenrollController.Withdraw(Student.studentId!);
            if (response)
            {
                await Notificator.ShowSuccess($"Hemos dado de baja con exito al estudiante {Student.FullName()}");
                StateHasChanged();
            }
        }
    }
    private async Task ChangeStudentState()
    {
        var desc = await Notificator.ShowAlertQuestion("Advertencia", $"¿Estas seguro que deseas cambiar es estado de: {Student.FullName()}?", ("Si","No"));
        if (desc)
        {
            var response = await UpdateController.UpdateStudentState(Student.studentId!);
            if (response)
            {
                await Notificator.ShowSuccess($"Hemos actualizado el estado del estudiante {Student.FullName()}");
                await LoadStudent();
            }
        }
    }
    private async Task UpdateEnrollment()
    {
        await Navigator.ShowModal("MoveStudentModal");
    }
    private async Task ViewPassword()
    {
        await Navigator.ShowModal("AccessInfoModal");
    }
    private async Task DownloadCertificate()
    {
        await PrintDocumentByStudentController.GetCertificate(Student.studentId!, Student.FullName());
    }
    private async Task DownLoadState()
    {
        await collectTariffController.GetAccountStatement(Student.studentId!); }
    private async Task PrintReportCard()
    {
        await Navigator.ShowModal("DowloadDegreeDocument");
    }    
    private async Task PrintProforma()
    {
        await PrintDocumentByStudentController.GetProforma(Student.studentId!);
    }
    private Task HandlePdfDocumentChanged(byte[] newPdf)
    {
        PdfDocument = newPdf;
        return Task.CompletedTask;
    }

    private async Task OpenAcademicReportModal()
    {
        await Navigator.ShowModal("ModalAcademicReport");
    }
}