using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    protected SchoolYearEntity SchoolYearEntity;
    
    protected GradeDto SelectedGrade;
    protected SubjectDto SubjectNew;
    protected SchoolYearTariffs SelectedTariff;

    protected override async Task OnParametersSetAsync()
    {
        SubjectNew = new SubjectDto();
        SelectedTariff = new SchoolYearTariffs();
        SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
        
        SchoolYearEntity Default = new SchoolYearEntity();
        SchoolYearEntity = await Controller.GetNewSchoolYears(Default);
        if (SchoolYearEntity == Default)
        {
            throw new InternalException("Es posible que exista mas de 2 años lectivos activos al mismo tiempo.");
        }
    }

    protected async Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {

            var response = await Controller.SaveNewSchoolYear(schoolYearEntity);

            if (response)
            {
                await Notificator.ShowSuccess("Éxito", "");
            }
            else
            {
                await Notificator.ShowError("");
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
                await Notificator.ShowSuccess("Éxito", response.Message);
            }
            else
            {
                await Notificator.ShowError("Error", response.Message);
            }
        }
        catch (Exception e)
        {
            await Notificator.ShowError("Error", e.Message);
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
            var response = await Controller.CreateNewTariff(tariff);

            if (response)
            {
                await Notificator.ShowSuccess("Éxito", "Tarifa guardada correctamente");
            }
            else
            {
                await Notificator.ShowError("Error", "No pudimos guardar la tarifa");
            }
    }

    
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }

}