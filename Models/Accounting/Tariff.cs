namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;


public class Tariff
{
    public int tariffId { get; set; }
    public string concept { get; set; } = null!;
    public double amount { get; set; }

    public Tariff () : this(0,"",0.0)
    {

    }

    public Tariff(int tariffId, string concept, double amount)
    {
        this.tariffId = tariffId;
        this.concept= concept;
        this.amount = amount;
    }
}