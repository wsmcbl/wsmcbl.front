namespace wsmcbl.src.Model.Secretary;

public class StudentTutor
{
    public string tutorId { get; set; }
    public string name { get; set; }
    public string phone { get; set; }

    public bool IsTutorValid()
    {
        return !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phone);
    }
}