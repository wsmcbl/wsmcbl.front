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

        var result = await controller.GetEnrollmentList(teacherId);
        foreach (var degree in degreeList)
        {
            enrollmentList.AddRange(result.Where(e => e.degreeId == degree.degreeId)
                    .OrderBy(e => e.position));
        }
        
        SetupDegreeColorList(degreeList);
    }

    private Dictionary<string, string> degreeColorList { get; set; } = new();

    private void SetupDegreeColorList(List<DegreeEntity> degreeList)
    {
        List<string> colorList = [
            "#4e73df",  // Azul SB-Admin (excelente contraste)
            "#1cc88a",  // Verde esmeralda
            "#36b9cc",  // Turquesa
            "#f6c23e",  // Amarillo (necesitará texto oscuro)
            "#e74a3b",  // Rojo coral
            "#5a5c69",  // Gris oscuro
            "#2e59d9",  // Azul oscuro
            "#17a673",  // Verde oscuro
            "#2c9faf",  // Turquesa oscuro
            "#dda20a",  // Amarillo mostaza
            "#be2617",  // Rojo vino
            "#6f42c1"   // Púrpura
        ];
        
        var colorListQuantity = colorList.Count;

        for (var i = 0; i < degreeList.Count; i++)
        {
            degreeColorList[degreeList[i].degreeId!] = colorList[i % colorListQuantity];
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