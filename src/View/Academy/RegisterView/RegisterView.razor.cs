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
    private string? TeacherId { get; set; }
    private TeacherEntity Teacher { get; set; } = new();
    private List<PartialEntity> Partial { get; set; } = new();
    private bool IsGuide  { get; set; }
    private int CurrentPartialId { get; set; } = 1;
    
    
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
        if (!Teacher.isGuide)
        {
            return;
        }
        
        IsGuide = Teacher.isGuide;
        Enrollment = await GuideController.GetMyEnrollmentGuide(TeacherId);
        Partial = await AddingStudentGradesController.GetPartialList();
        CurrentPartialId = Partial.FirstOrDefault(t => t.isActive)?.partialId ?? 1;            
            
        if (Enrollment.studentList.Count > 0)
        {
            Enrollment.OrderStudentList();
        }
    }

}