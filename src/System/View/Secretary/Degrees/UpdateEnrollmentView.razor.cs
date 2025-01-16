using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class UpdateEnrollmentView : BaseView
{
    [Parameter] public string degreeId { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected UpdateOfficialEnrollmentController? Controller { get; set; }
    protected string? TeacherFlags { get; set; } = "N/A";
    protected string? EnrollmentFlags { get; set; } = "N/A";
    protected string? SubjectFlags { get; set; } = "N/A";
    protected string? SubjectChangeName { get; set; } = "N/A";

    protected int NumberEnrollment;
    protected int Counter;
    protected int Counter2;
    protected List<TeacherEntity>? TeacherList;
    protected DegreeEntity DegreeEntity = null!; //validar que tenga Enrollments y que tenga la tupla
    protected UpdateEnrollmentDto UpdateEnrollmentDto = new();

    
    protected override async Task OnParametersSetAsync()
    {
        Counter = 0;
        Counter2 = 0;
        await LoadDegree();
    }

    protected async Task LoadDegree()
    {
        DegreeEntity = await Controller!.GetConfigureEnrollment(degreeId);
        TeacherList = await Controller!.GetTeacherList();
        
        UpdateEnrollmentDto = await Controller.GetDataOfGrade(degreeId);
        
        List<(SubjectEntity subject, TeacherEntity teacher)> subjectTeacherPairs = DegreeEntity.SubjectList!
            .Zip(TeacherList, (subject, teacher) => (subject, teacher))
            .ToList();
        
        
        foreach (var enrollment in UpdateEnrollmentDto.enrollmentList!)
        {
            enrollment.SetSubjectTeacherList(subjectTeacherPairs);
        }
    }

    protected string GetSemesterLabel(int semester)
    {
        return semester switch
        {
            1 => "Primer Semestre",
            2 => "Segundo Semestre",
            _ => "Ambos Semestres",
        };
    }

    protected async Task ChangeTeacherGuide(string? teacherID, string enrollmentNumber)
    {
        TeacherFlags = TeacherList!.FirstOrDefault(t => t.teacherId == teacherID)?.fullName;
        EnrollmentFlags = enrollmentNumber;
        await Navigator.ShowModal("EditTeacherGuideModal"); 
    }

    protected async Task UpdateTeacher(string subjectId, string subjectName, string enrollmentId)
    {
        SubjectFlags = subjectId;
        EnrollmentFlags = enrollmentId;
        SubjectChangeName = subjectName;
        await Navigator.ShowModal("UpdateTeacherSubject");
    }

    public async Task ConfigureDegree()
    {
        if (!await ValidateInformation())
        {
            return;
        }  
        
        var response = await Controller!.PutSaveEnrollment(DegreeEntity!);
        
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Las matrículas fueron actualizadas correctamente");
            await LoadDegree();
            StateHasChanged();
        }
    }

    private async Task<bool> ValidateInformation()
    {
        if (DegreeEntity!.EnrollmentList!.Any(entity => entity.capacity < 10))
        {
            await Notificator.ShowInformation("Error", "La capacidad de la sección debe ser al menos de 10.");
            return false;
        }

        if (DegreeEntity!.EnrollmentList!.Any(entity => string.IsNullOrWhiteSpace(entity.section)))
        {
            await Notificator.ShowInformation("Error", "El número del aula no puede estar vacío.");
            return false;
        }

        return true;
    }


    protected void OnTeacherChanged(EnrollmentEntity enrollment, SubjectEntity subject, string selectedTeacherId)
    { 
        for (var index = 0; index < enrollment.SubjectTeacherList.Count; index++)
        {
            var tuple = enrollment.SubjectTeacherList[index];
            
            if (tuple.subject.subjectId != subject.subjectId)
            {
                continue;
            }
            
            var selectedTeacher = TeacherList!.FirstOrDefault(t => t.teacherId == selectedTeacherId);
            if (selectedTeacher != null)
            {
                enrollment.SubjectTeacherList[index] = (tuple.subject, selectedTeacher);
            }
            break;
        }
    }

    protected override bool IsLoading()
    {   
        return !(DegreeEntity?.EnrollmentList != null && TeacherList!.Count != 0);
    }
}