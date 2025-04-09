using System.Text.Json.Serialization;

namespace wsmcbl.src.View.Academy.Statistics;

public class CualitativeDetailsDto
{
    [JsonPropertyName("aa")] public GenderStats AA { get; set; } = new();
    [JsonPropertyName("as")] public GenderStats AS { get; set; } = new();
    [JsonPropertyName("af")] public GenderStats AF { get; set; } = new();
    [JsonPropertyName("ai")] public GenderStats AI { get; set; } = new();
    [JsonPropertyName("quantity")] public GenderStats Quantity { get; set; } = new();

}