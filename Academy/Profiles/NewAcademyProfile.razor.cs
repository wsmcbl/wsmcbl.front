using Microsoft.AspNetCore.Components;
using wsmcbl.front.Accounting;

namespace wsmcbl.front.Academy;

using wsmcbl.front.Controllers;
using wsmcbl.front.model.Secretary.Output;

public class NewAcademyProfiles_razor : ComponentBase
{
    [Inject] protected AcademyController controller { get; set; } = null!;
    [Inject] protected AlertService alertService { get; set; } = null!;
    
    protected string FirsName;
    protected string SecondName;
    protected string FirsSurName;
    protected string SecondSurName;
    
    protected string Tutor;
    protected bool Sex;
    protected DateTime Birthday;

    protected List<string> sexo = new List<string> { "Femenino", "Masculino"};

    protected void GetSexo(ChangeEventArgs e)
    {
        var selectSexo = e.Value.ToString();

        if(selectSexo == "Femenino")
        {
            Sex = false;
        }else{
            Sex = true;
        }
    }

    protected async Task Save()
    {
        StudentEntityDto NewStudent = new StudentEntityDto
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

        var result = await controller.PostNewStudent(NewStudent);
        
        if (result == true)
        {
            await alertService.AlertSuccess("Agregado Exitosamente", "El estudiante fue registrado correctamente");
        }
        else
        {
            await alertService.AlertError("Error", "El estudiante no puedo ser registrado.");
        }

    }
}