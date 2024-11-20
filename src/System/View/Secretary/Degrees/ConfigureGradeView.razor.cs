using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ConfigureGrade : BaseView
{
    [Parameter] public string EnrollmentNumber { get; set; } = null!;
    [Parameter] public string GradeId { get; set; } = null!;
    
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateOfficialEnrollmentController? Controller { get; set; }

    protected int NumberEnrollment;
    protected int counter;
    protected int counter2;
    protected List<TeacherEntity>? TeacherList;
    protected DegreeEntity? DegreeEntity; 

    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        await loadDegree();
        TeacherList = await Controller!.GetTeacherList();
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }

    private async Task loadDegree()
    {
        DegreeEntity = await Controller!.GetConfigureEnrollment(GradeId);
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
        if (!await ValidateInformation())
        {
            return;
        }  
        
        var response = await Controller!.PutSaveEnrollment(DegreeEntity!);
        
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Las matrículas fueron actualizadas correctamente");
            await loadDegree();
            StateHasChanged();
        }
    }

    private async Task<bool>  ValidateInformation()
    {
        if (DegreeEntity!.EnrollmentList!.Any(entity => entity.Capacity < 10))
        {
            await Notificator.ShowInformation("Error", "La capacidad de la sección debe ser al menos de 10.");
            return false;
        }

        if (DegreeEntity!.EnrollmentList!.Any(entity => string.IsNullOrWhiteSpace(entity.Section)))
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
            
            if (tuple.subject.SubjectId != subject.SubjectId)
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
    
    protected void OnTeacherGuideChanged(EnrollmentEntity enrollment, string selectedTeacherId)
    {
        enrollment.teacherId = selectedTeacherId;
    }
    
    protected override bool IsLoad()
    {   
        return !(NumberEnrollment > 0 && DegreeEntity!.EnrollmentList != null && TeacherList!.Count != 0);
    }
}