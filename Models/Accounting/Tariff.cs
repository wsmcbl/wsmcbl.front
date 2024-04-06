namespace wsmcbl.front.Models.Accounting;

public class Tariff
{
    public int Tariffid { get; set; }
    public string Concept { get; set; } = null!;
    public double Amount { get; set; }


    public Tariff(string concept, double amount)
    {
        Concept= concept;
        Amount = amount;
    }


}