using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentListByTeacher;

public partial class EnrollmentListView : BaseView
{ 
    [Inject] private AddingStudentGradesController controller { get; set; } = null!;
    
    private TeacherEntity? teacher { get; set; }
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = [];
    
    protected override async Task OnInitializedAsync()
    {
        var teacherId = await controller.GetTeacherId();
        teacher = await controller.GetTeacherById(teacherId);
        EnrollmentList = await controller.GetEnrollmentList(teacherId);
    }

    protected override bool IsLoading()
    {
        return teacher == null;
    }

    private int getEnrollmentCount() => EnrollmentList.Count;
}