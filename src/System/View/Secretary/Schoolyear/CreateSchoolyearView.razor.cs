using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public partial class CreateSchoolyearView : BaseView
{
    [Inject] protected CreateSchoolyearController schoolyearController { get; set; } = null!;
    [Inject] protected CreateTariffDataController tariffDataController { get; set; } = null!;
    [Inject] protected CreateSubjectDataController subjectDataController { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    
    
    private DegreeSubjectDto? SelectedDegree { get; set; }
    private List<PartialToCreateDto>? PartialList { get; set; }
    private List<TariffDtoTemplate>? TariffList { get; set; }
    private List<DropDownItem> TariffTypeList { get; set; } = new();
    private List<DegreeSubjectDto> degreeList { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        TariffTypeList = await tariffDataController.GetTariffTypeList();
        var list  = await subjectDataController.GetDegreeDataList();
        degreeList = [];

        if (TariffTypeList == null)
        {
            throw new InternalException("Es posible que exista más de 2 años lectivos activos al mismo tiempo.");
        }

        ConfigInformation();
    }

    private void ConfigInformation()
    {
        PartialList =
        [
            new PartialToCreateDto { semester = 1, partial = 1 },
            new PartialToCreateDto { semester = 1, partial = 2 },
            new PartialToCreateDto { semester = 2, partial = 1 },
            new PartialToCreateDto { semester = 2, partial = 2 }
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
            PartialList![index].startDate = new DateOnlyDto(selectedDate);
        }
        else
        {
            PartialList![index].deadLine = new DateOnlyDto(selectedDate);
        }
    }

    private async Task CreateSchoolyear()
    {
        var list = TariffList!.Select(e => e.ToEntity()).ToList();
        var schoolyear = new SchoolyearToCreateDto(list, PartialList!);
        
        var response = await schoolyearController.CreateSchoolyear(schoolyear);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha creado el año lectivo correctamente.");
        }
        else
        {
            await Notificator.ShowError("Hubo un fallo al crear el año lectivo.");
        }
    }

    private void SelectGrade(DegreeSubjectDto degree)
    {
        SelectedDegree = degree;
    }

    private void RemoveSubject(SubjectDto subject)
    {
        if (SelectedDegree != null)
        {
            SelectedDegree.subjectList.Remove(subject);
        }
    }

    private string GetSelectedClass(DegreeSubjectDto degree)
    {
        return degree == SelectedDegree ? "selected-grade" : string.Empty;
    }

    protected override bool IsLoading()
    {
        return TariffList == null || PartialList == null;
    }
}