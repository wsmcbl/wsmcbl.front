using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears;

namespace wsmcbl.src.View.Secretary.Profiles;

public partial class CreateStudentProfile : ComponentBase
{
    [Inject] protected CreateStudentProfileController Controller { get; set; }
    [Inject] protected Navigator Navigator { get; set; }
    protected NewStudentDto NewStudent = new();
    protected bool selectSex;
    protected int selectModality;
    protected DateOnly dateonCreate;
    
    protected List<(bool Id, string Gender)> sexo;
    protected List<(int Id, string Modality)> modalitySelect;
    
    protected override void OnInitialized()
    {
        sexo = new List<(bool Id, string Gender)>
        {
            (false, "Femenino"),
            (true, "Masculino")
        };

        modalitySelect = new List<(int Id, string Modality)>
        {
            (1, "Preescolar"),
            (2, "Primaria"),
            (3, "Secundaria")
        };

        NewStudent.tutor = new TutorToCreateDto
        {
            name = "Sin Asignar",
            phone = "Sin Asignar"
        };
        
        dateonCreate = DateOnly.FromDateTime(DateTime.Today);
        selectSex = false; 
        selectModality = 1;
    }
    
    protected async Task SaveStudent()
    {
        NewStudent.student.birthday = MapperDate.ConvertDateOnlyToDate(dateonCreate);
        NewStudent.student.sex = selectSex;
        NewStudent.educationalLevel = selectModality;
        NewStudent.student.studentId = "";
        
        var resonse = await Controller.CreateNewStudent(NewStudent);

        if (resonse !=null)
        {
            Navigator.ToPage($"/accouting/tariffcollection/{resonse}");
        }
        
    }
}