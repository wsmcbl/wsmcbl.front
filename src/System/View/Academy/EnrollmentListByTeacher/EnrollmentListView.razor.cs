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
        partialList = await controller.GetPartialList();

        await LoadEnrollmentList(teacherId);
    }

    private async Task LoadEnrollmentList(string teacherId)
    {
        var degreeList = await controller.GetSortedDegreeList();
        SetupDegreeColorList(degreeList);

        var result = await controller.GetEnrollmentList(teacherId);
        foreach (var degree in degreeList)
        {
            enrollmentList.AddRange(
                result.Where(e => e.degreeId == degree.degreeId)
                    .OrderBy(e => e.position));
        }
    }

    private Dictionary<string, string>? degreeColorList { get; set; }

    private void SetupDegreeColorList(List<DegreeEntity> degreeList)
    {
        degreeColorList = new Dictionary<string, string>();
        
        List<string> colorList = [ "red", "green", "blue", "yellow", "cyan", "magenta", "black", "white", "gray",
            "orange", "purple", "pink", "brown"];

        for (var i = 0; i < degreeList.Count; i++)
        {
            degreeColorList[degreeList[i].degreeId!] = colorList[i];
        }
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