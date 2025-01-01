using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Academy.AddGrade;

public partial class AddGrade : ComponentBase
{
    [Inject] private AddStudentGradeController GradeController { get; set; } = null!;
    [Inject] private MoveTeacherGuideFromEnrollmentController TeacherController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string TeacherId { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string? DegreeName { get; set; }
    [Parameter] public string EnrollmentId { get; set; } = null!;
    
    private List<PartialEntity> partialsList = [];
    private List<TeacherEntity> TeacherListAvailable = [];

    private List<SubjectEntity>? subjectList { get; set; }
    private List<StudentEntity>? studentList { get; set; } = [];
    
    private int currentPartial;
    private int ActiveTabId = 1;
    private string? TeacherName = string.Empty;


    protected override async Task OnParametersSetAsync()
    {
        await GetPartials();
        await GetTeachersAvailable();
        GetNameOfTeacher();
        
        var activePartial = partialsList.FirstOrDefault(t => t.isActive);
        currentPartial = activePartial?.partialId ?? 1;
        
        await GetFullInfoEnrollment();
        ActiveTabId = currentPartial;
    }

    private async Task GetPartials()
    {
        partialsList = await GradeController.GetPartialsList();
    }
    
    private async Task GetTeachersAvailable()
    {
        TeacherListAvailable = await TeacherController.GetActiveTeachers();
    }
    
    private void GetNameOfTeacher()
    {
        TeacherName = TeacherListAvailable.FirstOrDefault(t => t.teacherId == TeacherId)?.fullName;
    }
    
    private async Task GetFullInfoEnrollment()
    {
        var result = await GradeController
            .GetFullInformationOfEnrollment(getTeacherEnrollmentByPartialDto());
        
        subjectList = result.subjectList;
        studentList = result.studentList;
    }

    private async Task ToSaveGrade()
    {
        if (studentList == null || studentList.Count == 0)
        {
            await Notificator.ShowError("Error", "No hay estudiantes para guardar las calificaciones.");
            return;
        }
        
        var gradeList = new List<GradeEntity>();
        foreach (var student in studentList)
        {
            if (student.gradeList == null)
            {
                await Notificator.ShowError("Error", $"No hay calificaciones para el estudiante {student.fullName}.");
                return;
            }
            
            gradeList.AddRange(student.gradeList);
        }

        var response = await GradeController.UpdateGrade(getTeacherEnrollmentByPartialDto(), gradeList);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "Hemos guardado las calificaciones correctamente.");
            return;
        }

        await Notificator.ShowError("Error", "Tuvimos problemas al guardar las calificaciones.");
    }

    private TeacherEnrollmentByPartialDto getTeacherEnrollmentByPartialDto()
    {
        return new TeacherEnrollmentByPartialDto
        {
            teacherId = TeacherId,
            enrollmentId = EnrollmentId,
            partialId = currentPartial
        };
    }
}