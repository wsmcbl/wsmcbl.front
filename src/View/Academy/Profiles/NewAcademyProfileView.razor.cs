using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Academy.Profiles;

public class NewAcademyProfiles : ComponentBase
{
    [Inject] protected IEnrollStudentController controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;

    protected string FirsName;
    protected string SecondName;
    protected string FirsSurName;
    protected string SecondSurName;

    protected string Tutor;
    protected bool Sex;
    protected DateTime Birthday;

    protected List<string> sexo = new List<string> { "Femenino", "Masculino" };

    protected void GetSexo(ChangeEventArgs e)
    {
        var selectSexo = e.Value.ToString();

        if (selectSexo == "Femenino")
        {
            Sex = false;
        }
        else
        {
            Sex = true;
        }
    }

    protected async Task Save()
    {
        StudentDto NewStudent = new StudentDto
        {
            Name = FirsName,
            SecondName = SecondName,
            Surname = FirsSurName,
            SecondSurname = SecondSurName,
            Sex = Sex,
            Birthday = new Birthday
            {
                Year = Birthday.Year,
                Month = Birthday.Month,
                Day = Birthday.Day
            },
            Tutor = Tutor
        };

        var result = false;//await controller.PostNewStudent(NewStudent);

        if (result == true)
        {
            await Notificator.ShowSuccess("Agregado Exitosamente", "El estudiante fue registrado correctamente");
        }
        else
        {
            await Notificator.ShowError("Error", "El estudiante no puedo ser registrado.");
        }
    }
}