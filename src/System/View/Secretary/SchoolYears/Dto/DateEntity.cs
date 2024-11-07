namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class DateEntity
{
    public int year { get; set; }
    public int month { get; set; }
    public int day { get; set; }

    public DateEntity()
    {
    }

    public DateEntity(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    public DateEntity(DateOnly? date) : this((DateOnly)date!)
    {
        
    }
    
    public DateEntity(DateOnly date) : this(date.Year, date.Month, date.Day)
    {
    }

    public DateEntity(DateTime dateTime) : this(DateOnly.FromDateTime(dateTime))
    {
    }

    public DateOnly ToDateOnly()
    {
        return new DateOnly(year, month, day);
    }
}