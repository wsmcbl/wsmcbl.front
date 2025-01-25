using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentListByTeacher;

public partial class EnrollmentListView : BaseView
{ 
    [Inject] private AddingStudentGradesController controller { get; set; } = null!;
    
    private TeacherEntity? teacher { get; set; }
    private List<EnrollmentByTeacherDto> enrollmentList { get; set; } = [];
    private List<PartialEntity> partialList { get; set; } = [];
    
    protected override async Task OnInitializedAsync()
    {
        var teacherId = await controller.GetTeacherId();
        teacher = await controller.GetTeacherById(teacherId);
        
        var result = await controller.GetEnrollmentList(teacherId);
        enrollmentList = result.OrderBy(e => e.degreeId).ToList();

        partialList = await controller.GetPartialList();
    }

    protected override bool IsLoading()
    {
        return teacher == null;
    }

    private int GetEnrollmentCount() => enrollmentList.Count;

    private string GetActivePartial()
    {
        var activePartial = partialList.FirstOrDefault(t => t.isActive);
        return activePartial != null ? activePartial.label : "No hay parcial activo";
    }
}