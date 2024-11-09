namespace wsmcbl.src.View.Secretary.Profiles;

public class TutorToCreateDto
{
    public string name { get; set; }
    public string? phone { get; set; }

    public TutorToCreateDto()
    {
        name = "";
        phone = "";
    }

    public bool IsTutorValid()
    {
        return !string.IsNullOrWhiteSpace(name);
    }
    
}
