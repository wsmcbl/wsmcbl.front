using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class TariffDto
{
    public int tariffId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }

    public TariffEntity ToEntity()
    {
        return new TariffEntity
        {
            tariffId = tariffId,
            schoolYear = schoolyearId,
            type = type,
            concept = concept,
            amount = amount,
            DueDate = dueDate?.ToDateOnly(),
            isLate = isLate
        };
    }
}

