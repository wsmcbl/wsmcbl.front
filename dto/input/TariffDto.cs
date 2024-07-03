using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

public class TariffDto
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
    
}

