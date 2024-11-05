using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
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
    protected SchoolYearEntity SchoolYearEntity;
    protected List<PartialListDto> PartialListDtos { get; set; }
    
    protected GradeDto SelectedGrade;
    protected List<DropdownList> DropdownTypeTariffsLists = new();
    protected ModalEditTariff modalEditTariffRef { get; set; }

    
    
    
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
        foreach (var item in SchoolYearEntity.tariffList)
        {
            if (item.dueDate != null)
            {
                item.OnlyDate = new DateOnly(item.dueDate.year, item.dueDate.month, item.dueDate.day);
            }
            else
            {
                item.OnlyDate = DateOnly.MinValue;
            }
        }
        
        PartialListDtos = new List<PartialListDto>
        {
            new() { semester = 1, partial = 1 },
            new() { semester = 1, partial = 2 },
            new() { semester = 2, partial = 1 },
            new() { semester = 2, partial = 2 },
        };
        return Task.CompletedTask;
    }
    protected void OnDateChanged(ChangeEventArgs e, int index, bool isStartDate)
    {
        if (DateTime.TryParse(e.Value.ToString(), out DateTime selectedDate))
        {
            if (isStartDate)
            {
                PartialListDtos[index].startDate.year = selectedDate.Year;
                PartialListDtos[index].startDate.month = selectedDate.Month;
                PartialListDtos[index].startDate.day = selectedDate.Day;
            }
            else
            {
                PartialListDtos[index].deadLine.year = selectedDate.Year;
                PartialListDtos[index].deadLine.month = selectedDate.Month;
                PartialListDtos[index].deadLine.day = selectedDate.Day;
            }
        }
    }
    protected async Task SaveSchoolYear()
    {
            var response = await Controller.SaveNewSchoolYear(SchoolYearEntity, PartialListDtos);
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
        SchoolYearEntity.tariffList?.Remove(tariff);
    }
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }
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
    
}