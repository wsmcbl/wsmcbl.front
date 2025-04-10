namespace wsmcbl.src.View.Components.UnenrollmentsView;

public class UnenrollmentBasicDto
{
    public int withdrawnId { get; set; }
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string lastEnrollmentId { get; set; } = null!;
    public string lastEnrollmentLabel { get; set; } = null!;
    public string schoolyearId { get; set; } = null!;
    public DateTime enrolledAt { get; set; }
    public DateTime withdrawnAt { get; set; }
}