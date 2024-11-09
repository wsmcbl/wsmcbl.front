using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.Profiles;

public partial class CreateStudentProfile : ComponentBase
{
    [Inject] protected CreateStudentProfileController Controller { get; set; }
    [Inject] protected Navigator Navigator { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    
    protected NewStudentDto NewStudent { get; set; } = null!;
    protected List<(bool Id, string Gender)> sex { get; set; } = null!;
    protected List<(int Id, string Modality)> modalitySelect { get; set; } = null!;
    private string MinDate => DateTime.Today.AddYears(-6).ToString("yyyy-MM-dd");
    
    [Parameter] public EventCallback onNewStudentCreated { get; set; }


    private void OnDateChanged(ChangeEventArgs e)
    { 
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime selectedDate))
        {
            NewStudent.student.birthday = new DateEntity(selectedDate);
        }
    }
    
    private void OnSexChanged(ChangeEventArgs e)
    {
        NewStudent.student.sex = bool.Parse(e.Value.ToString());
    }    
    private void OnModalityChanged(ChangeEventArgs e)
    {
        NewStudent.educationalLevel = int.Parse(e.Value.ToString());
    }
    
    protected override void OnInitialized()
    {
        sex =
        [
            (false, "Femenino"),
            (true, "Masculino")
        ];

        modalitySelect =
        [
            (1, "Preescolar"),
            (2, "Primaria"),
            (3, "Secundaria")
        ];
        
        NewStudent = new NewStudentDto
        {
            student = new StudentBasicDto(),
            tutor = new TutorToCreateDto(),
            educationalLevel = 1
        };
        NewStudent.student.sex = true;
    }

    
    private async Task<bool> IsInformationNotValid()
    {
        if (!NewStudent.IsNameValid())
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "El primer nombre y el primer apellido son obligatorios");
            return true;
        }

        if (!NewStudent.IsTutorValid())
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "El nombre del tutor es obligatorio");
            return true;
        }

        if (!NewStudent.IsBirthdayValid())
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "La fecha debe estar en el rango correcto");
            return true;
        }
        
        return false;

    }
    
    protected async Task SaveStudent()
    {
        if (await IsInformationNotValid())
        {
            return;
        }        
        
        var response = await Controller.CreateNewStudent(NewStudent);
        
        if (response ==null)
        {
           return;
        }
        
        var election =  await Notificator.ShowConfirmationQuestion("Se creó un nuevo perfil", "Selecciona la opción deseada",
            ("Ir a cobros","Cerrar"));

        if (election)
        {
            await Navigator.HideModal("NewStudentModal");
            Navigator.ToPage($"/accounting/tariffcollection/{response}");
        }
        else
        {
            await Navigator.HideModal("NewStudentModal");
            await onNewStudentCreated.InvokeAsync();
        }
    }
}