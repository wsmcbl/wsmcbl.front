namespace wsmcbl.front.Models.Accounting;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string enrollmentLabel {get; set;} = null!;
    public string fullName { get; set; } = null!;
    public string schoolyear { get; set; } = null!;
    public string tutor { get; set; } = null!;

    public int  areas { get; set; }
    public double discount { get; set; }
    public List<Transaction> transactions {get; set; } = null!;
    
}