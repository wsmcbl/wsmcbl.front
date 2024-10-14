using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ConfigureGrade : BaseView
{
    [Parameter] public string EnrollmentNumber { get; set; }
    [Parameter] public string GradeId { get; set; }
    
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }

    protected int NumberEnrollment;
    protected int counter;
    protected int counter2;
    protected List<TeacherEntity> TeacherList;
    protected DegreeEntity DegreeEntity; 

    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        await loadDegree();
        TeacherList = await Controller.GetTeacherList();
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }

    private async Task loadDegree()
    {
        DegreeEntity = await Controller.GetConfigureEnrollment(GradeId);
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

    public async Task ConfigureDegree()
    {
        var response = await Controller.PutSaveEnrollment(DegreeEntity);
        
        if (response)
        {
            Notificator.ShowSuccess("Exito", "Las matriculas fueron actualizadas correctamente");
            await loadDegree();
            StateHasChanged();
        }
    }
    
    protected void OnTeacherChanged(EnrollmentEntity enrollment, SubjectEntity subject, string selectedTeacherId)
    { 
        for (int i = 0; i < enrollment.SubjectTeacherList.Count; i++)
        {
            var tuple = enrollment.SubjectTeacherList[i];
            if (tuple.subject.SubjectId == subject.SubjectId)
            {
                var selectedTeacher = TeacherList.FirstOrDefault(t => t.teacherId == selectedTeacherId);
                if (selectedTeacher != null)
                {
                    enrollment.SubjectTeacherList[i] = (tuple.subject, selectedTeacher);
                }
                break;
            }
        }
    }
    
    protected void OnTeacherGuideChanged(EnrollmentEntity enrollment, string selectedTeacherId)
    {
        enrollment.teacherId = selectedTeacherId;
    }
    
    protected override bool IsLoad()
    {   
        return NumberEnrollment > 0 && DegreeEntity.EnrollmentList != null && TeacherList.Count != 0;
    }
}