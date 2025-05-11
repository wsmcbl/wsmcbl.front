using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.Utilities;

public class DropDownItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public DropDownItem()
    {
    }
    
    public DropDownItem(TariffTypeEntity value)
    {
        Id = value.typeId;
        Name = value.description;
    }
}