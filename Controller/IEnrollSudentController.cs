using wsmcbl.front.Service;
using wsmcbl.front.View.Secretary.EnrollmentStudent.Dto;
using StudentDto = wsmcbl.front.View.Academy.Profiles.StudentDto;

namespace wsmcbl.front.Controller;

public interface IEnrollSudentController
{
    Task<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>> GetStudents();
    Task<StudentFullDto> GetInfoStudent(string StudentID);
    Task<bool> PostNewStudent(StudentDto student);
    
}