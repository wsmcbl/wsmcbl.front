namespace wsmcbl.src.View.Components.Dto;

public class DateOnlyDto
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }

    public DateOnlyDto()
    {
    }
    
    public DateOnlyDto(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    public DateOnlyDto(DateOnly? date) : this((DateOnly)date!)
    {
        
    }
    
    public DateOnlyDto(DateOnly date) : this(date.Year, date.Month, date.Day)
    {
    }

    public DateOnlyDto(DateTime dateTime) : this(DateOnly.FromDateTime(dateTime))
    {
    }

    public DateOnly ToDateOnly()
    {
        return new DateOnly(year, month, day);
    }
    
    public bool IsNotDateValid()
    {
        var today = DateTime.Today;
        return year == today.Year && month == today.Month && day == today.Day;
    }
}