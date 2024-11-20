using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Model.Accounting;

public class TariffAuxEntity
{
    public string SchoolYear { get; set; } = null!;
    public string Concept { get; set; } = null!;
    public DateOnly? DueDate { get; set; }
    public int Type { get; set; }
    public Dictionary<int, double> amounts { get; set; }

    public TariffAuxEntity()
    {
        amounts = [];
    }
    
    public TariffAuxEntity(SchoolyearTariffDto schoolyearTariffDto)
    {
        SchoolYear = schoolyearTariffDto.schoolYear!;
        Concept = schoolyearTariffDto.concept!;
        DueDate = schoolyearTariffDto.dueDate == null ? null : schoolyearTariffDto.dueDate!.ToDateOnly();
        Type = schoolyearTariffDto.type;
        
        amounts = new Dictionary<int, double>
        {
            { 1, 0.0 },
            { 2, 0.0 },
            { 3, 0.0 }
        };
    }

    public List<SchoolyearTariffDto> getTariffDtoList()
    {
        var list = new List<SchoolyearTariffDto>();

        foreach (var item in amounts)
        {
            list.Add(new SchoolyearTariffDto
            {
                schoolYear = SchoolYear,
                concept = Concept,
                dueDate = DueDate == null ? null : new DateEntity(DueDate),
                type = Type,
                modality = item.Key,
                amount = item.Value
            });
        }
        
        return list;
    }
}