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
        var deadlineDate = DateTime.Parse(deadline);
        var timeRemaining = deadlineDate - DateTime.Now;

        if (timeRemaining.TotalDays > 1)
        {
            return $"Quedan {timeRemaining.Days} días";
        }
        else if (timeRemaining.TotalHours > 1)
        {
            return $"Quedan {timeRemaining.Hours} horas";
        }
        else if (timeRemaining.TotalMinutes > 1)
        {
            return $"Quedan {timeRemaining.Minutes} minutos";
        }
        else
        {
            return "¡Tiempo agotado!";
        }
    }
}