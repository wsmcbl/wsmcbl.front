using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Model.Secretary.Input;

namespace wsmcbl.front.View.Secretary.Grades;

public class ListGrades : ComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;
    [Parameter]
    public int SecctionsNumber { get; set; }
    [Parameter]
    public string GradeId { get; set; }
    protected bool tabsCreated;
    protected List<GradeEntity>? GradeList { get; set; }

    protected async Task OpenModal(int gradeId)
    {
        var GradeId = gradeId;
        await JSRuntime.InvokeVoidAsync("showModal", "confGrade");
    }
    
    protected void CreateTabs(int numberOfTabs, string gradeID)
    {
        GradeId = gradeID;
        if (numberOfTabs > 0 && numberOfTabs <= 7)
        {
            tabsCreated = true;
            OpenNewTab();
        }
        else
        {
            tabsCreated = false;
        }
    }
    
    protected async Task OpenNewTab()
    {
        await JSRuntime.InvokeVoidAsync("window.open", $"/secretary/grades/configuration/{SecctionsNumber}", "_blank");
    }
    
    protected override async Task OnInitializedAsync()
    {
        GradeList = await GetEnrollments();
    }
    private async Task<List<GradeEntity>?> GetEnrollments()
    {

        var grade1 = new GradeEntity()
        {
            GradeId = 1,
            Label = "Primer Grado",
            SchoolYear = "2023",
            Quantity = 1,
            Modality = "Primaria",
        };
        
        var grade2 = new GradeEntity()
        {
            GradeId = 2,
            Label = "Segundo Grado",
            SchoolYear = "2023",
            Quantity = 2,
            Modality = "Primaria",
        };
        
        var grade3 = new GradeEntity()
        {
            GradeId = 3,
            Label = "Septimo Grado",
            SchoolYear = "2023",
            Quantity = 0,
            Modality = "Secundaria",
        };
        
        var grade4 = new GradeEntity()
        {
            GradeId = 4,
            Label = "Octavo Grado",
            SchoolYear = "2023",
            Quantity = 0,
            Modality = "Secundaria",
        };

        return new List<GradeEntity> { grade1, grade2, grade3, grade4};
    }
    
    
}