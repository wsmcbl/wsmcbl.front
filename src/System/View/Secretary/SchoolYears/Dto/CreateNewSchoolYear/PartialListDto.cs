namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class PartialListDto
{
    public int partial { get; set; }
    public int semester { get; set; }
    public DateEntity startDate { get; set; }
    public DateEntity deadLine { get; set; }
    
    public PartialListDto()
    {
        partial = 0; 
        semester = 0; 
        startDate = new DateEntity(); 
        deadLine = new DateEntity(); 
    }
}