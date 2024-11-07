using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    protected SchoolYearEntity SchoolYear;
    protected List<PartialListDto> PartialListDtos { get; set; }
    
    protected DegreeDto SelectedDegree;
    protected List<DropdownList> DropdownTypeTariffsLists = new();
    protected ModalEditTariff? modalEditTariffRef { get; set; }

    
    
    
    protected override async Task OnParametersSetAsync()
    {
        var defaultSchoolyear = new SchoolYearEntity();
        SchoolYear = await Controller.GetNewSchoolYears(defaultSchoolyear);
        DropdownTypeTariffsLists = await Controller.GetTypeTariffList();
        
        if (SchoolYear == defaultSchoolyear || DropdownTypeTariffsLists == null)
        {
            throw new InternalException("Es posible que exista mas de 2 años lectivos activos al mismo tiempo.");
        }
        
        ConfigInformation();
    }
    
    private void ConfigInformation()
    { 
        SchoolYear.InitTariffAuxList();
        
        PartialListDtos = new List<PartialListDto>
        {
            new() { semester = 1, partial = 1 },
            new() { semester = 1, partial = 2 },
            new() { semester = 2, partial = 1 },
            new() { semester = 2, partial = 2 },
        };
    }
    protected void OnDateChanged(ChangeEventArgs e, int index, bool isStartDate)
    {
        if (!DateTime.TryParse(e.Value!.ToString(), out DateTime selectedDate))
        {
            return;
        }
        
        if (isStartDate)
        {
            PartialListDtos[index].startDate = new DateEntity(selectedDate);
        }
        else
        {
            PartialListDtos[index].deadLine = new DateEntity(selectedDate);
        }
    }
    protected async Task SaveSchoolYear()
    {
        SchoolYear.UpdateTariffList();
        var response = await Controller.SaveNewSchoolYear(SchoolYear, PartialListDtos);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "El año lectivo fue creado.");
        }
        else
        {
            await Notificator.ShowError("");
        }
    }
    protected void SelectGrade(DegreeDto degree)
    {
        SelectedDegree = degree;
    }
    protected void RemoveSubject(SubjectDto subject)
    {
        if (SelectedDegree != null)
        {
            SelectedDegree.subjects.Remove(subject);
        }
    }
    protected void RemoveTariff(TariffAuxEntity tariff)
    {
        SchoolYear.tariffAuxList.Remove(tariff);
    }
    protected string GetSelectedClass(DegreeDto degree)
    {
        return degree == SelectedDegree ? "selected-grade" : string.Empty;
    }
    protected async Task CallEditTariff(TariffAuxEntity item)
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
    
}