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
    [Inject] protected UpdateOfficialEnrollmentController Controller { get; set; } = null!;
    protected string? TeacherFlags { get; set; } = "N/A";
    protected string? EnrollmentFlags { get; set; } = "N/A";
    protected string? SubjectFlags { get; set; } = "N/A";
    protected string? SubjectChangeName { get; set; } = "N/A";

    protected int NumberEnrollment;
    protected int Counter;
    protected int Counter2;
    
    private List<EnrollmentEntity> enrollmentList { get; set; } = null!;
    private List<TeacherEntity> teacherList { get; set; } = null!;
    private List<SubjectEntity> subjectList { get; set; } = null!;

    
    protected override async Task OnParametersSetAsync()
    {
        Counter = 0;
        Counter2 = 0;
        await LoadDegree();
    }

    protected async Task LoadDegree()
    {
        var dto = await Controller.GetEnrollmentListByDegreeId(degreeId);
        enrollmentList = dto.enrollmentList;
        subjectList = dto.subjectList;

        teacherList = await Controller.GetTeacherList();
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

    private async Task ChangeTeacherGuide(string? teacherID, string enrollmentNumber)
    {
        TeacherFlags = teacherList.FirstOrDefault(t => t.teacherId == teacherID)?.fullName;
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

    private async Task UpdateEnrollmentList()
    {
        var resutl = await ValidateInformation();
    }

    private async Task<bool> ValidateInformation()
    {
        if (enrollmentList!.Any(entity => entity.capacity < 10))
        {
            await Notificator.ShowInformation("Error", "La capacidad de la sección debe ser al menos de 10.");
            return false;
        }

        if (enrollmentList!.Any(entity => string.IsNullOrWhiteSpace(entity.section)))
        {
            await Notificator.ShowInformation("Error", "El número del aula no puede estar vacío.");
            return false;
        }

        return true;
    }


    protected void OnTeacherChanged(EnrollmentEntity enrollment, SubjectEntity subject, string selectedTeacherId)
    { 
   
    }

    protected override bool IsLoading()
    {   
        return !(enrollmentList.Count != 0 && teacherList.Count != 0);
    }
}