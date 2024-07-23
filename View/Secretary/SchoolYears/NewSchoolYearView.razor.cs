using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.dto.Output;
using wsmcbl.front.Model.Secretary.Input;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public  class NewSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController SController { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; } = null!;

    protected SchoolYearEntity SchoolYearEntity;
    protected SchoolYearTariffs? SelectedTariff;
    protected TariffDataDto? NewTariffItem;    
    
    protected async Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        if (schoolYearEntity != null && schoolYearEntity.Tariffs != null)
        {
            foreach (var tariff in schoolYearEntity.Tariffs)
            {
                if (tariff.DueDate != null && 
                    tariff.DueDate.Year == 0 && 
                    tariff.DueDate.Month == 0 && 
                    tariff.DueDate.Day == 0)
                {
                    tariff.DueDate = null;
                }
            }
        }
        var obj = ConvertToNewSchoolYear(schoolYearEntity);
        var sending = await SController.SaveSchoolYear(obj);
    }
    public NewSchoolYearDto ConvertToNewSchoolYear(SchoolYearEntity schoolYearEntity)
    {
        return new NewSchoolYearDto()
        {
            Grades = schoolYearEntity.Grades,
            Tariffs = schoolYearEntity.Tariffs
        };
    }
    protected void NewTariff()
    {
        NewTariffItem = new TariffDataDto
        {
            Concept = string.Empty,
            Amount = 0,
            Type = 0,
            Modality = 0,
            dueDate = new Date()
            {
                Year = 0,
                Month = 0,
                Day = 0
            }
        };
    }

    protected async Task SaveNewTariff(TariffDataDto tariffsData)
    { 
        if (tariffsData?.dueDate != null &&
            tariffsData.dueDate.Year == 0 &&
            tariffsData.dueDate.Month == 0 &&
            tariffsData.dueDate.Day == 0)
        {
            tariffsData.dueDate = null;
        }

        var response = await SController.CreateNewTariff(tariffsData);

        if (response?.Success == true)
        {
            NewTariffItem = null;
            await AlertService.AlertSuccess("Éxito", response.Message);
        }
        else
        {
            await AlertService.AlertError("Error", response?.Message ?? "Error desconocido");
        }
    }
    
    protected void EditTariff(SchoolYearTariffs tariff)
    {
        SelectedTariff = tariff;
    }
    
    protected Task SaveEdit()
    {
        if (SelectedTariff != null)
        {
            SelectedTariff = null;
        }
        return Task.CompletedTask;
    }
    
    protected void Cancel()
    {
        SelectedTariff = null;
        NewTariffItem = null;
    }
}