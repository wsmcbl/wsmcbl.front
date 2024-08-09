using Newtonsoft.Json;

namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;

public class Date
{
    [JsonProperty("year")]
    public int Year { get; set; }
    
    [JsonProperty("month")]
    public int Month { get; set; }
    
    [JsonProperty("day")]
    public int Day { get; set; }

    public DateOnly ToDateOnly()
    {
        return new DateOnly(Year, Month, Day);
    }
    
    public DateOnly ToDateNow()
    {
        return new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
    
    
}