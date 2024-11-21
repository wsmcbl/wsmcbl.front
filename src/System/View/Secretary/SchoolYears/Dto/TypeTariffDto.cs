using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class TypeTariffDto
{
    public int typeId { get; set; }
    public string description { get; set; } = null!;
    
    public DropdownList ToDropdownList()
    {
        return new DropdownList()
        {
            Id = typeId,
            Name = description
        };
    }
}