using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.StudentList.ModifyEducationLevel;

public partial class ChangeEducationLevelView : ComponentBase
{
    [Parameter] public string? StudentId { get; set; } 
    [Inject] protected ChangeEducationLevelController controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    private StudentForEducationLevelDto Student = new();
    private int EducationLevelNow;

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrWhiteSpace(StudentId))
        {
            Student = await controller.GetStudent(StudentId);
            EducationLevelNow = Student.educationalLevel;
        }
    }

    private async Task Actualizar()
    {
        var response = await controller.ChangeEducationLevel(Student.studentId, Student.educationalLevel);
        if (response)
        {
            await Notificator.ShowSuccess("Exito","Hemos cambiado el nivel educativo.");
            await Navigator.HideModal("ChangeEducationLevelModal");
            return;
        }

        await Notificator.ShowError("Error",
            "Hemos tenido problemas al cambiar el nivel educativo del estudiante, asegurese de no haber realizado ningun cobro.");
    }
}