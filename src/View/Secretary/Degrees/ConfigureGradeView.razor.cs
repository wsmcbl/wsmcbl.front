using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ConfigureGrade : ComponentBase
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
    protected DegreeBasicDto? DegreeDto;
    protected DegreeEntity DegreeEntity; 

    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        DegreeDto = await Controller.ConfigureEnrollment(GradeId);
        DegreeEntity = DegreeDto.toEntity();
        
        TeacherList = await Controller.GetTeacherBasic();
        TeacherAvailable = TeacherList.Where(t => t.isGuide == false).ToList();
        
        
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }

    protected void ChangeTeacherGuideStatus(ChangeEventArgs e, EnrollmentBasicDto enrollment)
    {
        var selectedTeacherId = e.Value.ToString();

        foreach (var item in TeacherAvailable)
        {
            if (selectedTeacherId == item.TeacherId)
            {
                enrollment.teacherId = selectedTeacherId;
                enrollment.teacherName = item.Name;
            }
        }
        Console.WriteLine($"Item TeacherId: {enrollment.teacherId}, Selected TeacherId: {selectedTeacherId}");
    }
    
    protected void ChangeTeacherStatus(ChangeEventArgs e, SubjectBasicDto subject)
    {
        var selectedTeacherId = e.Value.ToString();

        foreach (var item in TeacherList)
        {
            if (selectedTeacherId == item.TeacherId)
            {
                subject.teacherId = selectedTeacherId;
            }
        }
    }
    
}