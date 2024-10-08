using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Secretary.Profiles;

public partial class CreateStudentProfile : ComponentBase
{
    [Inject] protected IEnrollStudentController controller { get; set; } = null!;
    [Inject] protected IJSRuntime JS { get; set; } = null!;
    private StudentDto Student = new();
    private bool selectSex;
    private int selectModality;

    private List<(bool Id, string Gender)> sexo = new()
    {
        (false, "Femenino"),
        (true, "Masculino")
    };
    
    private List<(int Id, string Modality)> modality = new()
    {
        (1, "Preescolar"),
        (2, "Primaria"),
        (3, "Secundaria")
    };

    protected async Task HandleValidSubmit()
    {
        Student.educationalLevel = selectModality;
        Student.sex = selectSex;
        
        Console.WriteLine(Student.name);
        Console.WriteLine(Student.secondName);
        Console.WriteLine(Student.surname);
        Console.WriteLine(Student.secondSurname);
        Console.WriteLine(Student.tutor);
        Console.WriteLine(Student.birthday);
        Console.WriteLine(Student.educationalLevel);
        Console.WriteLine(Student.sex);
    }
}