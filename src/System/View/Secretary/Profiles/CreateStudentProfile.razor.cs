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
        Student.Modality = selectModality;
        Student.Sex = selectSex;
        
        Console.WriteLine(Student.Name);
        Console.WriteLine(Student.SecondName);
        Console.WriteLine(Student.Surname);
        Console.WriteLine(Student.SecondSurname);
        Console.WriteLine(Student.Tutor);
        Console.WriteLine(Student.Birthday);
        Console.WriteLine(Student.Modality);
        Console.WriteLine(Student.Sex);
    }
}