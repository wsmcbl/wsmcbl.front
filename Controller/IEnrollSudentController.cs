using wsmcbl.front.View.Academy.Profiles;

namespace wsmcbl.front.Controller;

public interface IEnrollSudentController
{
    public void GetStudents();
    public Task<bool> PostNewStudent(StudentDto student);
}