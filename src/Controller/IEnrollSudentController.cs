using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

namespace wsmcbl.src.Controller;

public interface IEnrollSudentController
{
    Task<List<StudentDto>> GetStudents();
    Task<StudentFullDto> GetInfoStudent(string StudentID);
    
}