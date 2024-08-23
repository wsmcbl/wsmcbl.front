using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

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
    protected List<TeacherEntity> TeacherAvailable;
    protected DegreeEntity DegreeEntity; 

    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        DegreeEntity = await Controller.GetConfigureEnrollment(GradeId);
        
        TeacherList = await Controller.GetTeacherList();
        TeacherAvailable = TeacherList.Where(t => t.isGuide == false).ToList();
        
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }

    protected override bool IsLoad()
    {
        return NumberEnrollment > 0 && DegreeEntity.EnrollmentList != null && TeacherList.Count == 0;
    }

    protected void ChangeTeacherGuideStatus(ChangeEventArgs e, EnrollmentEntity enrollment)
    {
        var selectedTeacherId = e.Value.ToString();
        //#####
    }
    
    protected void ChangeTeacherStatus(ChangeEventArgs e, SubjectBasicDto subject)
    {
        var selectedTeacherId = e.Value.ToString();

        //####
    }
    
}