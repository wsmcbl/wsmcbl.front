namespace wsmcbl.src.Model.Secretary;

public class StudentTutor
{
    public string tutorId { get; set; } = null!;
    public string name { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string? email { get; set; }

    public bool IsTutorValid()
    {
        return !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phone);
    }
}