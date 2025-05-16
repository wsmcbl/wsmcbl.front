using System.Formats.Tar;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Components.Dto;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

public partial class CreateNewSchoolYearView : BaseView
{
    [Inject] private CreateTariffDataController CreateTariffDataController { get; set; } = null!;
    [Inject] private CreateSchoolyearController CreateSchoolYearController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    
    private CreateSchoolYearDto SchoolYear { get; set; } = new();
    private List<PartialsDto> PartialList = 
    [
        new() { semester = 1, partial = 1 },
        new() { semester = 1, partial = 2 },
        new() { semester = 2, partial = 1 },
        new() { semester = 2, partial = 2 }
    ];
    private List<DropDownItem> TariffTypeList { get; set; } = new();
    private List<CreateTariffDto> originalTariffList { get; set; } = new();
    private List<TariffDtoTemplate> TariffList { get; set; } = new();
    private List<TariffDto> TariffToCreateList { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        TariffTypeList = await CreateTariffDataController.GetTariffTypeList();
        originalTariffList = await CreateSchoolYearController.GetTariffList();
        ConfigInformation();
    }

    private void ConfigInformation()
    {
        foreach (var tariff in originalTariffList)
        {
            TariffList.Add(new TariffDtoTemplate
            {
                concept = tariff.concept,
                type = tariff.typeId,
                dueDate = tariff.dueDate?.ToDateOnlyDto(tariff.dueDate) ?? null
            });
        }
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
    
    private async Task CreateSchoolYear()
    {
        foreach (var tariff in TariffList)
        {
            TariffToCreateList.Add(new TariffDto
            {
                concept = tariff.concept ?? "N/A",
                amount = tariff.amount1,
                dueDate = tariff.dueDate.MapToDto(),
                typeId = tariff.type,
                educationalLevel = 1
            });
            
            TariffToCreateList.Add(new TariffDto
            {
                concept = tariff.concept ?? "N/A",
                amount = tariff.amount2,
                dueDate = tariff.dueDate.MapToDto(),
                typeId = tariff.type,
                educationalLevel = 2
            });
            
            TariffToCreateList.Add(new TariffDto
            {
                concept = tariff.concept ?? "N/A",
                amount = tariff.amount3,
                dueDate = tariff.dueDate.MapToDto(),
                typeId = tariff.type,
                educationalLevel = 3
            });
            
        }
        SchoolYear.partialList = PartialList;
        SchoolYear.tariffList = TariffToCreateList;
        var response = await CreateSchoolYearController.CreateSchoolYear(SchoolYear);
        if (response)
        {
            await Notificator.ShowSuccess("Hemos creado correctamente el nuevo año lectivo");
            return;
        }

        await Notificator.ShowError("Obtuvimos unos problemas para crear el nuevo año lectivo");
    }


    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private int currentStep = 1;
    private void NextStep()
    {
        if (currentStep < 3) currentStep++;
    }
    private void PrevStep()
    {
        if (currentStep > 1) currentStep--;
    }

    
 
}