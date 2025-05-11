using Newtonsoft.Json;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DetailDto
{
    public int tariffId { get; set; }
    public decimal amount { get; set; }
    public bool applyArrears { get; set; }
}