using Microsoft.AspNetCore.Components;
using wsmcbl.front.Accounting;
using wsmcbl.front.model.Academy.Input;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public partial class ConfigEnrollment_razor : ComponentBase
{
    [Parameter] public string? IdEnrollment { get; set; }
    [Inject] protected AlertService alertService { get; set; } = null!;
    
    protected List<EnrollmentEntity>? Enrollments { get; set; }
    protected EnrollmentEntity? Enrollment;

    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(IdEnrollment))
            {
                throw new ArgumentException("Emrollment Id is not valid");
            }
            
            Enrollment = await LoadEnrollment();
        }
        catch (ArgumentException ae)
        {
            await alertService.AlertWarning(ae.Message);
        }
        catch
        {
            await alertService.AlertWarning("Obtuvimos problemas al cargar los datos");
        }
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
        
        var enrollment = new EnrollmentEntity
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