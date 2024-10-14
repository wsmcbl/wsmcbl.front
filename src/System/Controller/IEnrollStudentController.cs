using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.Controller;

public interface IEnrollStudentController
{
    public Task<List<StudentDto>> GetStudents();
    public Task<StudentEntity> GetInfoStudent(string studentId);
    public Task<byte[]> GetEnrollSheetPdf(string studentId);
    public Task<List<DegreeBasicDto>> GetDegreeBasicList();
    public Task<bool> SaveEnrollment(StudentEntity student, string enrollmentId);
}