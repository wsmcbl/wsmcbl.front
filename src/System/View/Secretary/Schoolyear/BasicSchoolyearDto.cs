namespace wsmcbl.src.View.Secretary.Schoolyear;

public class BasicSchoolyearDto
{ 
    public string schoolyearId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string startDate { get; set; } = null!;
    public string deadLine { get; set; } = null!; 
    public bool isActive { get; set; }
}