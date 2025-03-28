using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Components.CreateStudent;

public partial class CreateStudentProfileView : ComponentBase
{
    [Inject] protected Navigator _navigator { get; set; } = null!;
    [Inject] protected Notificator _notificator { get; set; } = null!;
    [Inject] protected CreateStudentProfileController _controller { get; set; } = null!;
    
    [Parameter] public EventCallback onNewStudentCreated { get; set; }
    
    private StudentToCreateDto StudentToCreate { get; set; } = null!;
    private List<(bool Id, string Gender)> sex { get; set; } = null!;
    private List<(int Id, string Modality)> modalitySelect { get; set; } = null!;
    private string MaxDate => DateTime.Today.AddYears(-4).ToString("yyyy-MM-dd");

    private void OnDateChanged(ChangeEventArgs e)
    { 
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime selectedDate))
        {
            StudentToCreate.student.birthday = new DateOnlyDto(selectedDate);
        }
    }
    
    private void OnSexChanged(ChangeEventArgs e)
    {
        if(e.Value == null) return;
        StudentToCreate.student.sex = bool.Parse(e.Value.ToString()!);
    }    
    private void OnModalityChanged(ChangeEventArgs e)
    {
        if(e.Value == null) return;
        StudentToCreate.educationalLevel = int.Parse(e.Value.ToString()!);
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

        StudentToCreate = new StudentToCreateDto();
        StudentToCreate.SetAsDefault();
    }

    
    private async Task<bool> IsInformationNotValid()
    {
        if (!StudentToCreate.IsNameValid())
        {
            await _notificator.ShowInformation("El primer nombre y el primer apellido son obligatorios, ingreselos.");
            return true;
        }

        if (!StudentToCreate.IsTutorValid())
        {
            await _notificator.ShowInformation("El nombre del tutor es obligatorio.");
            return true;
        }

        if (!StudentToCreate.IsBirthdayValid())
        {
            await _notificator.ShowInformation("La fecha debe estar en el rango correcto.");
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
        
        var response = await _controller.CreateNewStudent(StudentToCreate);
        if (response == null)
        {
           return;
        }

        var options = ("Ir a cobros", "Cerrar");
        var election =  await _notificator
            .ShowConfirmationQuestion("Se creó el nuevo perfil.", "Seleccione la opción deseada", options);

        await _navigator.HideModal("NewStudentModal");
        StudentToCreate.SetAsDefault();
        
        if (election)
        {
            _navigator.ToPage($"/accounting/students/{response}");
        }
        else
        {
            await onNewStudentCreated.InvokeAsync();
        }
    }
}