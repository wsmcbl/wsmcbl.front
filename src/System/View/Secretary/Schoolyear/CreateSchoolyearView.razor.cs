using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Components.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public partial class CreateSchoolyearView : BaseView
{
    [Inject] protected CreateSchoolyearController schoolyearController { get; set; } = null!;
    [Inject] protected CreateTariffDataController tariffDataController { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private SchoolYearEntity? Schoolyear { get; set; }
    private List<PartialListDto>? PartialListDto { get; set; }

    private DegreeDto? SelectedDegree;
    private List<DropDownItem> TariffTypeItemList { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        var defaultSchoolyear = new SchoolYearEntity();
        Schoolyear = await schoolyearController!.GetNewSchoolYears(defaultSchoolyear);
        
        TariffTypeItemList = await tariffDataController.GetTariffTypeList();

        if (Schoolyear == defaultSchoolyear || TariffTypeItemList == null)
        {
            throw new InternalException("Es posible que exista más de 2 años lectivos activos al mismo tiempo.");
        }

        ConfigInformation();
    }

    private void ConfigInformation()
    {
        Schoolyear!.InitTariffAuxList();

        PartialListDto =
        [
            new PartialListDto { semester = 1, partial = 1 },
            new PartialListDto { semester = 1, partial = 2 },
            new PartialListDto { semester = 2, partial = 1 },
            new PartialListDto { semester = 2, partial = 2 }
        ];
    }

    private void OnDateChanged(ChangeEventArgs e, int index, bool isStartDate)
    {
        if (!DateTime.TryParse(e.Value!.ToString(), out var selectedDate))
        {
            return;
        }

        if (isStartDate)
        {
            PartialListDto![index].startDate = new DateOnlyDto(selectedDate);
        }
        else
        {
            PartialListDto![index].deadLine = new DateOnlyDto(selectedDate);
        }
    }

    private async Task CreateSchoolyear()
    {
        Schoolyear!.UpdateTariffList();
        var response = await schoolyearController!.CreateSchoolyear(Schoolyear, PartialListDto!);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha creado el año lectivo correctamente.");
        }
        else
        {
            await Notificator!.ShowError("Hubo un fallo al crear el año lectivo.");
        }
    }

    private void SelectGrade(DegreeDto degree)
    {
        SelectedDegree = degree;
    }

    private void RemoveSubject(SubjectDto subject)
    {
        if (SelectedDegree != null)
        {
            SelectedDegree.subjects.Remove(subject);
        }
    }

    private void RemoveTariff(TariffAuxEntity tariff)
    {
        Schoolyear!.tariffAuxList!.Remove(tariff);
    }

    private string GetSelectedClass(DegreeDto degree)
    {
        return degree == SelectedDegree ? "selected-grade" : string.Empty;
    }

    protected override bool IsLoading()
    {
        return Schoolyear == null;
    }
}