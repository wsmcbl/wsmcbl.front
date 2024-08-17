namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class Date
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }

    public DateOnly ToDateOnly()
    {
        return new DateOnly(year, month, day);
    }
    
    public DateOnly ToDateNow()
    {
        return new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
    
    
}