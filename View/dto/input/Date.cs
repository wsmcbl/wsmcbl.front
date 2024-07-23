using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

public class Date
{
    [JsonProperty("year")]
    public int Year { get; set; }
    
    [JsonProperty("month")]
    public int Month { get; set; }
    
    [JsonProperty("day")]
    public int Day { get; set; }
}