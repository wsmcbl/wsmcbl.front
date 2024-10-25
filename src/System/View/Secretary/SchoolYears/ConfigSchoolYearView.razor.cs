using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
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
    protected List<DropdownList> DropdownTypeTariffsLists = new();

    protected ModalEditTariff modalEditTariffRef { get; set; }

    
    
    protected async Task CallEditTariff(SchoolYearTariffs item)
    {
        if (modalEditTariffRef != null)
        {
            await modalEditTariffRef.EditTariff(item);
        }
    }
    protected void RefreshParentComponent()
    {
        StateHasChanged();
    }
    protected override async Task OnParametersSetAsync()
    {
        SchoolYearEntity Default = new SchoolYearEntity();
        SchoolYearEntity = await Controller.GetNewSchoolYears(Default);
        DropdownTypeTariffsLists = await Controller.GetTypeTariffList();
        
        if (SchoolYearEntity == Default || DropdownTypeTariffsLists == null)
        {
            throw new InternalException("Es posible que exista mas de 2 años lectivos activos al mismo tiempo.");
        }
        await ConfigInformation();
    }
    private Task ConfigInformation()
    { 
        foreach (var item in SchoolYearEntity.Tariffs)
        {
            if (item.DueDate != null)
            {
                item.OnlyDate = new DateOnly(item.DueDate.year, item.DueDate.month, item.DueDate.day);
            }
            else
            {
                item.OnlyDate = DateOnly.MinValue;
            }
        }

        return Task.CompletedTask;
    }
    protected async Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {
            var response = await Controller.SaveNewSchoolYear(schoolYearEntity);
            if (response)
            {
                await Notificator.ShowSuccess("Éxito", "El año lectivo fue creado.");
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
            SelectedGrade.subjects.Remove(subject);
        }
    }
    protected void RemoveTariff(SchoolYearTariffs tariff)
    {
        SchoolYearEntity.Tariffs?.Remove(tariff);
    }
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }
    
}