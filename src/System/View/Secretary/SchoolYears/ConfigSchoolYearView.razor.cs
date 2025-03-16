using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateSchoolyearController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    protected SchoolYearEntity? SchoolYear { get; set; }
    private List<PartialListDto>? PartialListDto { get; set; }
    
    protected DegreeDto? SelectedDegree;
    protected List<DropDownItem> TariffTypeItemList { get; set; } = new();
    protected ModalEditTariff? modalEditTariffRef { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        var defaultSchoolyear = new SchoolYearEntity();
        SchoolYear = await controller!.GetNewSchoolYears(defaultSchoolyear);
        TariffTypeItemList = await controller.GetTypeTariffList();
        
        if (SchoolYear == defaultSchoolyear || TariffTypeItemList == null)
        {
            throw new InternalException("Es posible que exista más de 2 años lectivos activos al mismo tiempo.");
        }
        
        ConfigInformation();
    }
    
    private void ConfigInformation()
    { 
        SchoolYear!.InitTariffAuxList();
        
        PartialListDto =
        [
            new PartialListDto { semester = 1, partial = 1},
            new PartialListDto { semester = 1, partial = 2 },
            new PartialListDto { semester = 2, partial = 1 },
            new PartialListDto { semester = 2, partial = 2 }
        ];
    }
    protected void OnDateChanged(ChangeEventArgs e, int index, bool isStartDate)
    {
        if (!DateTime.TryParse(e.Value!.ToString(), out DateTime selectedDate))
        {
            return;
        }
        
        if (isStartDate)
        {
            PartialListDto![index].startDate = new DateEntity(selectedDate);
        }
        else
        {
            PartialListDto![index].deadLine = new DateEntity(selectedDate);
        }
    }
    protected async Task SaveSchoolYear()
    {
        SchoolYear!.UpdateTariffList();
        var response = await controller!.CreateSchoolyear(SchoolYear, PartialListDto!);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha creado el año lectivo correctamente.");
        }
        else
        {
            await Notificator!.ShowError("Hubo un fallo al crear el año lectivo.");
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
        SchoolYear!.tariffAuxList!.Remove(tariff);
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