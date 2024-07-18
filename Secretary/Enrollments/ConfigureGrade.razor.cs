using Microsoft.AspNetCore.Components;
using wsmcbl.front.Accounting;
using wsmcbl.front.model.Academy.Input;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Secretary.Enrollments;

public class ConfigEnrollment_razor : ComponentBase
{
    [Parameter] public string Number { get; set; }
    [Inject] protected AlertService alertService { get; set; } = null!;
    
    protected List<model.Secretary.Input.Enrollments>? Enrollments { get; set; }
    protected model.Secretary.Input.Enrollments? Enrollment;
    protected int tabNumber;

    protected override async void OnParametersSet()
    {
        Enrollment = await LoadEnrollment();
        tabNumber = Convert.ToInt32(Number);
    }
    
    private async Task<model.Secretary.Input.Enrollments> LoadEnrollment()
    {
        var students = new List<StudentEntity>
        {
            new StudentEntity
            {
                StudentId = "S001",
                Name = "John",
                SecondName = "A.",
                Surname = "Doe",
                SecondSurname = "Smith",
                IsActive = true,
                SchoolYear = "2023",
                Tutor = "Mr. Tutor",
                Sex = true,
                Birthday = new DateOnly(2005, 6, 1),
                EnrollmentLabel = "Enrollment 1"
            },
            new StudentEntity
            {
                StudentId = "S002",
                Name = "Jane",
                SecondName = "B.",
                Surname = "Doe",
                SecondSurname = "Johnson",
                IsActive = true,
                SchoolYear = "2023",
                Tutor = "Mrs. Tutor",
                Sex = false,
                Birthday = new DateOnly(2006, 7, 15),
                EnrollmentLabel = "Enrollment 2"
            }
        };
        
        var subjects = new List<Subject>
        {
            new Subject
            {
                SubjectID = "SUB001",
                Name = "Mathematics",
                Description = "Basic Math",
                Modality = "Online"
            },
            new Subject
            {
                SubjectID = "SUB002",
                Name = "Science",
                Description = "Basic Science",
                Modality = "Offline"
            }
        };
        
        var teacher = new Teacher
        {
            TeacherId = "T001",
            Name = "Mr. Smith",
            Phone = "123-456-7890",
            Subjects = subjects
        };
        
        var enrollment = new model.Secretary.Input.Enrollments
        {
            EnrollmentId = "1",
            Label = "Primer Grado",
            SchoolYear = "2023",
            Students = students,
            TeacherGuide = teacher,
            Subjects = subjects,
            Capacity = 200
        };

        return enrollment;
    }
    
}