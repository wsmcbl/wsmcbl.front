using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Secretary.Input;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public partial class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected AlertService AlertService { get; set; }
    protected SchoolYearEntity SchoolYearEntity;
    protected GradeDto SelectedGrade;

    protected override void OnParametersSet()
    {
        try
        {
            SchoolYearEntity = Controller.NewSchoolYears();
            AlertService.AlertSuccess("Datos cargados", "Edite los datos correspondiente y de click en guardar");
        }
        catch (Exception e)
        {
            AlertService.AlertError("Error al cargar", "Puede ser que ya existan mas de 2 años lectivos activos");
        }
    }
    protected Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        throw new NotImplementedException();
    }
    
    protected void SelectGrade(GradeDto grade)
    {
        SelectedGrade = grade;
    }
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }
    
    
    
    
}