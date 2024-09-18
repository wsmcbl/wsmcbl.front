using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public interface IEnrollStudentController
{
    public Task<List<StudentDto>> GetStudents();
    public Task<StudentEntity> GetInfoStudent(string StudentID);
    public Task<byte[]?> GetPdfContent(string StudentID);
    public Task<List<DegreeBasicDto>> GetDegreeBasicList();
}