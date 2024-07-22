using System.Text;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using wsmcbl.front.Accounting;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.input;
using wsmcbl.front.dto.Output;

namespace wsmcbl.front.Secretary.Components;

public partial class Test : ComponentBase
{
    protected TariffDataDto? NewTariffItem;
    [Inject] protected CreateOfficialEnrollmentController controller { get; set; }
    [Inject] protected AlertService AlertService { get; set; }

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
    protected async Task SaveNewTariff(TariffDataDto tariffDataDto)
    { 
        if (tariffDataDto?.dueDate != null && tariffDataDto.isUndefine())
        {
            tariffDataDto.dueDate = null;
        }

        var response = await controller.createNewTariff(tariffDataDto);

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
    
    
    protected void Cancel()
    {
        NewTariffItem = null;
    }
    
}