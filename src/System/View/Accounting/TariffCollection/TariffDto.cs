using Newtonsoft.Json;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class TariffDto
{
    [JsonProperty("tariffId")]
    public int TariffId { get; set; }

    [JsonProperty("schoolyearId")]
    public string SchoolYear { get; set; } = null!;

    [JsonProperty("concept")]
    public string Concept { get; set; } = null!;

    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("dueDate")]
    public DateOnlyDto? DueDate { get; set; }

    [JsonProperty("isLate")]
    public bool IsLate { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }
    
}

