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


    private void OnDateChanged(ChangeEventArgs e)
    { 
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime selectedDate))
        {
            NewStudent.student.birthday = new DateEntity(selectedDate);
            Console.WriteLine(NewStudent.student.birthday.day);
            Console.WriteLine(NewStudent.student.birthday.month);
            Console.WriteLine(NewStudent.student.birthday.year);
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

    private static bool IsDateValidate(DateEntity studentBirthday)
    {
        return studentBirthday.year != DateTime.Now.Year || studentBirthday.month != DateTime.Now.Month || studentBirthday.day != DateTime.Now.Day;
    }

    private async Task<bool> ValidateInformation()
    {
        if (string.IsNullOrWhiteSpace(NewStudent.student.name) || string.IsNullOrWhiteSpace(NewStudent.student.surname))
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "El primer nombre y el primer apellido son obligatorios");
            return false;
        }

        if (string.IsNullOrWhiteSpace(NewStudent.tutor.name))
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "El nombre del tutor es obligatorio");
            return false; 
        }

        if (!IsDateValidate(NewStudent.student.birthday))
        {
            await Notificator.ShowInformation("Favor ingrese los campos solicitados", "La fecha debe estar en el rango correcto");
            return false; 
        }

        return true;
    }
    
    protected async Task SaveStudent()
    {
        if (!await ValidateInformation())
        {
            return;
        }        
        
        var response = await Controller.CreateNewStudent(NewStudent);
        
        if (response !=null)
        {
            var election =  await Notificator.ShowConfirmationQuestion("Se creó un nuevo perfil", "selecciona la opción deseada",
                ("Ir a cobros","Cerrar"));

            if (election)
            {
                await Navigator.HideModal("NewStudentModal");
                Navigator.ToPage($"/accounting/tariffcollection/{response}");
            }
            else
            {
                await Navigator.HideModal("NewStudentModal");   
                StateHasChanged();
            }
        }
    }
}