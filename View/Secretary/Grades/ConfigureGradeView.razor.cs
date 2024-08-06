using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Secretary;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.Grades;

public class ConfigureGrade : ComponentBase
{
    [Parameter] public string EnrollmentNumber { get; set; }
    [Parameter] public string GradeId { get; set; }
    
    [Inject] protected AlertService alertService { get; set; } = null!;
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }

    protected EnrollmentEntity? Enrollment;
    protected DegreeEntity? Grade;
    
    protected int NumberEnrollment;
    protected int counter;
    protected int counter2;
    protected string SchoolYear;
    
    protected override async Task OnParametersSetAsync()
    {
        counter = 0;
        counter2 = 0;
        Grade = await Controller.ConfigureEnrollment(GradeId);
        SchoolYear = Grade.SchoolYear;
        NumberEnrollment = Convert.ToInt32(EnrollmentNumber);
    }
}