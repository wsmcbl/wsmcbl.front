namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class TariffCreateNewSchoolYearDto
{
    public string schoolYear { get; set; }
    public string? concept { get; set; }
    public double amount { get; set; }
    public DateEntity DueDateEntity { get; set; }
    public int type { get; set; }
    public int modality { get; set; }
}