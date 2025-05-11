using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.GetDateRecordingGrade;

public partial class GetDateRecordingGradeComponent : ComponentBase
{
    [Inject] private EnablePartialGradeRecordingController Controller { get; set; } = null!;
    private RecondingGradeDateDto? MaxDate { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        MaxDate = await Controller.GetEnablePartial();
    }
    
    private string GetTimeRemaining()
    {
        if (!DateTime.TryParse(MaxDate!.gradeRecordDeadline, out var deadlineDate))
        {
            return "Formato de fecha no válido";
        }

        var timeRemaining = deadlineDate - DateTime.Now;

        if (timeRemaining.TotalMilliseconds <= 0)
        {
            return "¡Tiempo agotado!";
        }

        if (timeRemaining.TotalDays >= 2)
        {
            return $"Quedan {timeRemaining.Days} días";
        }
        if (timeRemaining.TotalHours >= 2)
        {
            var hours = (int)timeRemaining.TotalHours;
            return $"Quedan {hours} horas";
        }
        
        if (timeRemaining.TotalMinutes >= 1)
        {
            var minutes = (int)timeRemaining.TotalMinutes;
            return $"Quedan {minutes} minutos";
        }
        
        return "Quedan menos de un minuto";
    }
}