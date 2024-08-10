using wsmcbl.src.Service;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using StudentDto = wsmcbl.src.View.Academy.Profiles.StudentDto;

namespace wsmcbl.src.Controller;

public interface IEnrollSudentController
{
    Task<List<View.Secretary.EnrollmentStudent.Dto.StudentDto>> GetStudents();
    Task<StudentFullDto> GetInfoStudent(string StudentID);
    Task<bool> PostNewStudent(StudentDto student);
    Task<byte[]?> GetPdfContent(string studentId);

}