using Microsoft.AspNetCore.Components;
using wsmcbl.front.Model.Secretary.Input;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Secretary.Enrollments;

public class ConfigEnrollment : ComponentBase
{
    [Parameter] public string Number { get; set; }
    [Inject] protected AlertService alertService { get; set; } = null!;
    
    protected List<EnrollmentEntity>? Enrollments { get; set; }
    protected EnrollmentEntity? Enrollment;
    protected int tabNumber;

    protected override async void OnParametersSet()
    {
        Enrollment = await LoadEnrollment();
        tabNumber = Convert.ToInt32(Number);
    }
    
    private async Task<EnrollmentEntity> LoadEnrollment()
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
        
        var subjects = new List<SubjectEntity>
        {
            new SubjectEntity
            {
                SubjectID = "SUB001",
                Name = "Mathematics",
                Description = "Basic Math",
                Modality = "Online"
            },
            new SubjectEntity
            {
                SubjectID = "SUB002",
                Name = "Science",
                Description = "Basic Science",
                Modality = "Offline"
            }
        };
        
        var teacher = new TeacherEntity()
        {
            TeacherId = "T001",
            Name = "Mr. Smith",
            Phone = "123-456-7890",
            Subjects = subjects
        };
        
        var enrollment = new EnrollmentEntity()
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