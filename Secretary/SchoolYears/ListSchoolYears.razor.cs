using Microsoft.AspNetCore.Components;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Secretary.SchoolYears;

public class ListSchoolYears_razor : ComponentBase
{
    protected List<SchoolYearEntity>? SchoolYear;

    protected override async void OnInitialized()
    {
        SchoolYear = await GetSchoolYear();
    }

    private Task<List<SchoolYearEntity>> GetSchoolYear()
    {
        var years = new SchoolYearEntity()
        {
            SchoolYearId = "001",
            Label = "2023",
            StartDate = new DateOnly(2023, 1, 1),
            DeadLine = new DateOnly(2023,12,31),
            IsActive = false,
        };
        return Task.FromResult(new List<SchoolYearEntity> { years });
    }
}