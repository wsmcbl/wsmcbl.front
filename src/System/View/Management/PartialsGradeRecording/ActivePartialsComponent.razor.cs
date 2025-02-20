using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public partial class ActivePartialsComponent : ComponentBase
{
    [Parameter] public int PartialId {get; set;}
    [Parameter] public string? PartialName {get; set;}
    [Parameter] public string? Semester {get; set;}
    [Parameter] public string? DateRange {get; set;}
    private bool Enabled {get; set;} = true;
    
    
    
    private DateTime DeadLine { get; set; } = DateTime.Now.AddHours(1);
    private DateTime DeadLineMax { get; set; } = DateTime.Now.AddDays(15);
    private DateTime DeadLineMin { get; set; } = DateTime.Now.AddHours(1);

    private string FormatDateTime(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm");
    
    
    protected override Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    private void ActivePartials()
    { 
        Console.Write(DeadLine);
    }
}