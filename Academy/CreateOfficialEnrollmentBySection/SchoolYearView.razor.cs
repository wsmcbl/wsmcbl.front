using Microsoft.AspNetCore.Components;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public class SchoolYearView_razor : ComponentBase
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
            Label = "Primer Grado",
            StartDate = new DateOnly(2023, 1, 1),
            DeadLine = new DateOnly(2023,12,31),
            IsActive = false,
        };
        return Task.FromResult(new List<SchoolYearEntity> { years });
    }
}