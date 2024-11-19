using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Model.Secretary;

public class SchoolYearEntity
{
    public string? id { get; set; }
    public string label { get; set; } = null!;
    public string? startDate { get; set; }
    public string? deadLine { get; set; }
    public bool isActive { get; set; }
    public double exchangeRate { get; set; }
    public List<DegreeDto>? degreeList { get; set; }
    public List<SchoolyearTariffDto>? tariffList { get; set; }
    
    public List<TariffAuxEntity>? tariffAuxList { get; set; }

    public void InitTariffAuxList()
    {
        tariffAuxList = new List<TariffAuxEntity>();
        
        var newTariffList = tariffList!.Where(e => e.modality == 3).ToList();
        foreach (var item in newTariffList)
        {
            tariffAuxList.Add(new TariffAuxEntity(item));
        }
    }

    public void UpdateTariffList()
    {
        tariffList = new List<SchoolyearTariffDto>();
        foreach (var item in tariffAuxList!)
        {
            tariffList.AddRange(item.getTariffDtoList());
        }
    }
}