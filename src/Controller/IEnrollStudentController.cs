using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

namespace wsmcbl.src.Controller;

public interface IEnrollStudentController
{
    public Task<List<StudentDto>> GetStudents();
    public Task<StudentFullDto> GetInfoStudent(string StudentID);
    public Task<byte[]?> GetPdfContent(string StudentID);
}