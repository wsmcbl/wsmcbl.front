namespace wsmcbl.src.Model.Academy;

public class NullTeacherEntity : TeacherEntity
{
    public NullTeacherEntity()
    {
        teacherId = "N/A";
        fullName = "No Asignado";
        isGuide = false;
        Phone = "Sin Celular";
        SubjectList = [];
    }
}