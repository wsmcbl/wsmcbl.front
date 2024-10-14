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
    protected bool selectSex { get; set; }
    protected int selectModality { get; set; }
    protected DateOnly dateonCreate { get; set; }

    protected List<(bool Id, string Gender)> sex { get; set; } = null!;
    protected List<(int Id, string Modality)> modalitySelect { get; set; } = null!;
    
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
        
        dateonCreate = DateOnly.FromDateTime(DateTime.Today);
        selectSex = false; 
        selectModality = 1;
    }
    
    protected async Task SaveStudent()
    {
        NewStudent.student.birthday = new DateEntity(dateonCreate);
        NewStudent.student.sex = selectSex;
        NewStudent.educationalLevel = selectModality;
        
        var response = await Controller.CreateNewStudent(NewStudent);

        if (response !=null)
        {
            await Notificator.ShowSuccess("Se creo un nuevo perfil", "El perfil del estudiante fue creado exitosamente.");
            await Navigator.HideModal("NewStudentModal");
            Navigator.ToPage($"/accounting/tariffcollection/{response}");
        }
    }
}