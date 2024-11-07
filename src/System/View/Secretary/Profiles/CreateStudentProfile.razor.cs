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
    protected int selectModality { get; set; }
    protected List<(bool Id, string Gender)> sex { get; set; } = null!;
    protected List<(int Id, string Modality)> modalitySelect { get; set; } = null!;
    private string MinDate => DateTime.Today.AddYears(-6).ToString("yyyy-MM-dd");


    private void OnDateChanged(ChangeEventArgs e)
    {
        if (!DateTime.TryParse(e.Value!.ToString(), out DateTime selectedDate))
        {
            NewStudent.student.birthday = new DateEntity(selectedDate);
        }
    }
    private void OnSexChanged(ChangeEventArgs e)
    {
        NewStudent.student.sex = bool.Parse(e.Value.ToString());
        Console.WriteLine(NewStudent.student.sex);
    }    
    private void OnModalityChanged(ChangeEventArgs e)
    {
        NewStudent.educationalLevel = int.Parse(e.Value.ToString());
        Console.WriteLine(NewStudent.educationalLevel);
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
    
    protected async Task SaveStudent()
    {
        if (string.IsNullOrWhiteSpace(NewStudent.student.name) && string.IsNullOrWhiteSpace(NewStudent.student.surname))
        {
            Notificator.ShowInformation("Favor ingrese los campos solicitados", "El primer nombre y el primer apellido son obligatorios");
            return;
        }
        
        var response = await Controller.CreateNewStudent(NewStudent);

        if (response !=null)
        {
            await Notificator.ShowSuccess("Se creo un nuevo perfil", "El perfil del estudiante fue creado exitosamente.");
            await Navigator.HideModal("NewStudentModal");
            Navigator.ToPage($"/accounting/tariffcollection/{response}");
        }
    }
}