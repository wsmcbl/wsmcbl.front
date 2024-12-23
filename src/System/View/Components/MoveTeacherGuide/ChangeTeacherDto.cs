namespace wsmcbl.src.View.Components.MoveTeacherGuide;

public class ChangeTeacherDto
{
    public string? enrollmentId { get; set; }
    public string newTeacherId { get; set; }

    public ChangeTeacherDto()
    {
        enrollmentId = string.Empty;
        newTeacherId = string.Empty;
    }
}