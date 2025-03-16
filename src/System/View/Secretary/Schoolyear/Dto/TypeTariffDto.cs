using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class TypeTariffDto
{
    public int typeId { get; set; }
    public string description { get; set; } = null!;
    
    public DropDownItem ToDropdownList()
    {
        return new DropDownItem()
        {
            Id = typeId,
            Name = description
        };
    }
}