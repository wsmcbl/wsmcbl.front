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
    
    private StudentForEducationLevelDto Student { get; set; } = new();
    private int EducationLevelNow { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrWhiteSpace(StudentId))
        {
            return;
        }

        await LoadStudent();
    }

    private async Task LoadStudent()
    {
        Student = await controller.GetStudent(StudentId);
        EducationLevelNow = Student.educationalLevel;
        StateHasChanged();
    }

    private async Task UpdateLevel()
    {
        var response = await controller.ChangeEducationLevel(Student.studentId, Student.educationalLevel);
        if (!response)
        {
            await Notificator.ShowError(
                "Hemos tenido problemas al cambiar el nivel educativo del estudiante, asegurese de no haber realizado ningun cobro.");
            return;
        }
        
        await LoadStudent();
        await Notificator.ShowSuccess("Hemos cambiado el nivel educativo.");
        await Navigator.HideModal("ChangeEducationLevelModal");
    }
}