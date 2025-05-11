using wsmcbl.src.Model.Academy;
using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.Details;

public class SchoolYearFullDto
{
    public string schoolyearId { get; set; } = "N/A";
    public string label { get; set; } = "N/A";
    public string startDate { get; set; } = "N/A";
    public string deadLine { get; set; } = "N/A";    
    public bool isActive { get; set; }
    public ExchangeRateEntity exchangeRate { get; set; } = new();
    public List<PartialEntity> partialList { get; set; } = new();
    public List<DegreeEntity> degreeList { get; set; } = new();
    public List<TariffEntity> tariffList { get; set; } = new();
}