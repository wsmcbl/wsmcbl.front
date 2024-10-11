using Newtonsoft.Json;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DetailDto
{
    public int tariffId { get; set; }
    public double amount { get; set; }
    public bool applyArrears { get; set; }
}