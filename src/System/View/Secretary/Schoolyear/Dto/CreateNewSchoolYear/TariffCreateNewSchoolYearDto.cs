namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class TariffCreateNewSchoolYearDto
{
    public string schoolYear { get; set; }
    public string? concept { get; set; }
    public double amount { get; set; }
    public DateEntity dueDate { get; set; }
    public int type { get; set; }
    public int modality { get; set; }

    public TariffCreateNewSchoolYearDto(SchoolyearTariffDto schoolyearTariff)
    {
        schoolYear = schoolyearTariff.schoolYear!;
        concept = schoolyearTariff.concept;
        amount = schoolyearTariff.amount;
        dueDate = schoolyearTariff.dueDate!;
        type = schoolyearTariff.type;
        modality = schoolyearTariff.modality;
    }
}