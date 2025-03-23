using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.GetDateRecordingGrade;

public partial class GetDateRecordingGradeComponent : ComponentBase
{
    [Inject] GetDateMaxOfRecordingGradeController Controller { get; set; } = null!;
    private RecondingGradeDateDto? MaxDate { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        MaxDate = await Controller.GetMaxDate();
    }
    
    private string CalculateTimeRemaining(string deadline)
    {
        if (!DateTime.TryParse(deadline, out var deadlineDate))
        {
            return "Formato de fecha no válido";
        }

        var timeRemaining = deadlineDate - DateTime.Now;

        if (timeRemaining.TotalMilliseconds <= 0)
        {
            return "¡Tiempo agotado!";
        }
        else if (timeRemaining.TotalDays >= 2)
        {
            return $"Quedan {timeRemaining.Days} días";
        }
        else if (timeRemaining.TotalHours >= 2)
        {
            int hours = (int)timeRemaining.TotalHours;
            return $"Quedan {hours} horas";
        }
        else if (timeRemaining.TotalMinutes >= 1)
        {
            int minutes = (int)timeRemaining.TotalMinutes;
            return $"Quedan {minutes} minutos";
        }
        else
        {
            return "Quedan menos de un minuto";
        }
    }
}