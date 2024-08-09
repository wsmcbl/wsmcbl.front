using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controller;
using wsmcbl.front.dto.Output;
using wsmcbl.front.Model.Secretary;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public partial class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected AlertService AlertService { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    protected SchoolYearEntity SchoolYearEntity;
    
    protected GradeDto SelectedGrade;
    protected SubjectDto SubjectNew;
    protected SchoolYearTariffs SelectedTariff;
    
    protected override void OnParametersSet()
    {
        try
        {
            SubjectNew = new SubjectDto();
            
            SelectedTariff = new SchoolYearTariffs();
            SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
            
            SchoolYearEntity = Controller.NewSchoolYears();
            
            AlertService.AlertSuccess("Datos cargados", "Edite los datos correspondiente y de click en guardar");
        }
        catch (Exception e)
        {
            AlertService.AlertError("Error al cargar", "Puede ser que ya existan mas de 2 años lectivos activos");
        }
    }

    protected async Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        try
        {
            var response = await Controller.SaveNewSchoolYear(schoolYearEntity);

            if (response.Success)
            {
                await AlertService.AlertSuccess("Éxito", response.Message);
            }
            else
            {
                await AlertService.AlertError("Error", response.Message);
            }
        }
        catch (Exception e)
        {
            await AlertService.AlertError("Error", e.Message);
        }
    }

    protected void SelectGrade(GradeDto grade)
    {
        SelectedGrade = grade;
    }

    protected void RemoveSubject(SubjectDto subject)
    {
        if (SelectedGrade != null)
        {
            SelectedGrade.Subjects.Remove(subject);
        }
    }

    protected async Task SaveNewSubject(SubjectDto subject)
    {
        try
        {
            var response = await Controller.CreateNewSubject(subject);

            if (response.Success)
            {
                await AlertService.AlertSuccess("Éxito", response.Message);
            }
            else
            {
                await AlertService.AlertError("Error", response.Message);
            }
        }
        catch (Exception e)
        {
            await AlertService.AlertError("Error", e.Message);
        }
    }

    protected async Task EditTariff(SchoolYearTariffs tariff)
    {
        SelectedTariff = tariff;
        SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
        await JsRuntime.InvokeVoidAsync("eval", "$('#ModalEditTariff').modal('show');");
    }
    
    protected void RemoveTariff(SchoolYearTariffs tariff)
    {
        SchoolYearEntity.Tariffs?.Remove(tariff);
    }

    protected async Task SaveNewTariff(SchoolYearTariffs tariff)
    {
        try
        {
            TariffDataDto tariffDataDto = MapperDate.MapToTariffDataDto(tariff);
            
            var response = await Controller.CreateNewTariff(tariffDataDto);

            if (response.Success)
            {
                await AlertService.AlertSuccess("Éxito", response.Message);
            }
            else
            {
                await AlertService.AlertError("Error", response.Message);
            }
        }
        catch (Exception e)
        {
            await AlertService.AlertError("Error", e.Message);
        }
    }

    
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }

}