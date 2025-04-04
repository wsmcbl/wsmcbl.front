using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.EnrollmentGuide;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.RegisterView;

public partial class RegisterView : BaseView
{
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;
    [Inject] private ViewEnrollmentGuideController GuideController { get; set; } = null!;
    [Inject] private JwtClaimsService JwtClaimsService { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private EnrollmentDto Enrollment { get; set; } = new();
    private int CurrentPartial { get; set; } = 1;
    private string? TeacherId { get; set; }
    private TeacherEntity Teacher { get; set; } = new();
    private bool Isguide  { get; set; }

    
    protected override async Task OnInitializedAsync()
    {
        var token = await JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrEmpty(token))
        {
            await Notificator.ShowWarning("Advertencia", "Estamos teniendo problemas para obtener tu información," +
                                                         "intentelo mas tarde");
            return;
        }

        TeacherId = token;
        Teacher = await AddingStudentGradesController.GetTeacherById(TeacherId);
        if (Teacher.isGuide)
        {
            Isguide = true;
            Enrollment = await GuideController.GetMyEnrollmentGuide(TeacherId);
            if (Enrollment?.studentList?.Count > 0)
            {
                Enrollment.studentList = Enrollment.studentList.OrderBy(s => s.sex).ThenBy(s => s.fullName).ToList();
            }
        
            if (Enrollment?.studentList == null)
            {
                if (Enrollment != null) Enrollment.studentList = new List<StudentDto>();
            }
        }
        
    }
    
    private async Task ViewPerformanceReport()
    {
        await Navigator.ShowModal("ReportGradeModal");
    }

}