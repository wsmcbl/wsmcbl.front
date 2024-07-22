using Microsoft.AspNetCore.Components;
using wsmcbl.front.Accounting;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.input;
using wsmcbl.front.dto.Output;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Secretary.SchoolYears;

public  class NewSchoolYear_razor : ComponentBase
{
    [Inject] protected SecretaryController SController { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; } = null!;

    protected SchoolYearEntity SchoolYearEntity;
    protected SchoolYearTariffs? SelectedTariff;
    protected NewTariffDto? NewTariffItem;    
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            SchoolYearEntity = await SController.NewSchoolYears();
            if (SchoolYearEntity != null)
            {
                if (SchoolYearEntity.Tariffs != null)
                {
                    foreach (var tariff in SchoolYearEntity.Tariffs.Where(tariff => tariff.DueDate == null))
                    {
                        tariff.DueDate = new Date()
                        {
                            Year = 0,
                            Month = 0,
                            Day = 0
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AlertService.AlertError("Error", $"Error al obtener los datos: {ex.Message}");
        }
    }
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
        NewTariffItem = new NewTariffDto
        {
            Concept = string.Empty,
            Amount = 0,
            Type = 0,
            Modality = 0,
            DueDate = new Date()
            {
                Year = 0,
                Month = 0,
                Day = 0
            }
        };
    }

    protected async Task SaveNewTariff(NewTariffDto tariffs)
    { 
        if (tariffs?.DueDate != null &&
            tariffs.DueDate.Year == 0 &&
            tariffs.DueDate.Month == 0 &&
            tariffs.DueDate.Day == 0)
        {
            tariffs.DueDate = null;
        }

        var response = await SController.NewTariff(tariffs);

        if (response?.Success == true)
        {
            NewTariffItem = null;
            StateHasChanged();
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