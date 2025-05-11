using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Components.Dto;
using wsmcbl.src.View.Management.PartialsGradeRecording;
using wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public class SchoolyearDto
{
    public string schoolyearId { get; set; } = null!;
    public string label { get; set; } = null!;
    public DateOnlyDto startDate { get; set; } = null!;
    public DateOnlyDto deadLine { get; set; } = null!;
    public bool isActive { get; set; }

    public ExchangeRateEntity? exchangeRate { get; set; }

    public List<PartialDto>? partialList { get; set; }
    
    public List<DegreeSubjectDto>? degreeList { get; set; }
    
    public List<TariffDto>? tariffList { get; set; }    
}