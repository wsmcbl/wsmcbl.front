namespace wsmcbl.front.Models.Accounting;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string enrollmentLabel {get; set;} = null!;
    public string schoolyear { get; set; } = null!;
    public string tutor { get; set; } = null!;
    public double discount { get; set; }
    public bool isActive { get; set; }
    
    public List<PaymentDto> debtHistory {get; set; } = null!;
    
}