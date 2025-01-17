using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.CreateStudent;

public partial class CreateStudentProfileView : ComponentBase
{
    [Inject] protected CreateStudentProfileController? _controller { get; set; }
    [Inject] protected Navigator? _navigator { get; set; }
    [Inject] protected Notificator? _notificator { get; set; }
    
    protected StudentToCreateDto StudentToCreate { get; set; } = null!;
    protected List<(bool Id, string Gender)> sex { get; set; } = null!;
    protected List<(int Id, string Modality)> modalitySelect { get; set; } = null!;
    private string MaxDate => DateTime.Today.AddYears(-4).ToString("yyyy-MM-dd");
    
    [Parameter] public EventCallback onNewStudentCreated { get; set; }


    private void OnDateChanged(ChangeEventArgs e)
    { 
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime selectedDate))
        {
            StudentToCreate.student.birthday = new DateEntity(selectedDate);
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
            await _notificator!.ShowInformation("El primer nombre y el primer apellido son obligatorios, ingreselos.");
            return true;
        }

        if (!StudentToCreate.IsTutorValid())
        {
            await _notificator!.ShowInformation("El nombre del tutor es obligatorio.");
            return true;
        }

        if (!StudentToCreate.IsBirthdayValid())
        {
            await _notificator!.ShowInformation("La fecha debe estar en el rango correcto.");
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
        
        var response = await _controller!.CreateNewStudent(StudentToCreate);
        if (response == null)
        {
           return;
        }

        var options = ("Ir a cobros", "Cerrar");
        var election =  await _notificator!
            .ShowConfirmationQuestion("Se creó el nuevo perfil.", "Seleccione la opción deseada", options);

        await _navigator!.HideModal("NewStudentModal");
        StudentToCreate.SetAsDefault();
        
        if (election)
        {
            _navigator.ToPage($"/accounting/tariffcollection/{response}");
        }
        else
        {
            await onNewStudentCreated.InvokeAsync();
        }
    }
}