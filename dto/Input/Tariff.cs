namespace wsmcbl.front.dto.Input;
using Newtonsoft.Json;


public class Tariff
{
    [JsonProperty("tariffId")]
    public int TariffId { get; set; }

    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;

    [JsonProperty("concept")]
    public string Concept { get; set; } = null!;

    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("dueDate")]
    public string DueDate { get; set; }

    [JsonProperty("isLate")]
    public bool IsLate { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }


    public Tariff () : this(0,"","",0.0,"1999-01-01",false,0)
    {

    }

    public Tariff(int tariffId, string schoolYear, string concept, double amount, string dueDate, bool isLate, int type)
    {
        TariffId = tariffId;
        SchoolYear = schoolYear;
        Concept= concept;
        Amount = amount;
        DueDate = dueDate;
        IsLate = isLate;
        Type = type;
    }
}

