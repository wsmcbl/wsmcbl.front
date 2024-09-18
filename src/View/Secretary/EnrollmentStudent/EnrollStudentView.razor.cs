using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent;

public class EnrollStudent : ComponentBase
{
    [Parameter] public string StudentId { get; set; }
    [Inject] protected IEnrollStudentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    [Inject] protected Navigator Navigator { get; set; }

    protected StudentEntity Student;
    protected int Age;
    protected string Sex;
    protected string SelectActive;
    
    protected List<DegreeBasicDto> Degrees = new();
    protected List<EnrollmentsBasicDto> CurrentEnrollments = [];
    protected int CurrentEnrollmentCapacity, CurrentEnrollmentQuantity;

    protected override async Task OnParametersSetAsync()
    {
        Student = await Controller.GetInfoStudent(StudentId);
        Degrees = await Controller.GetDegreeBasicList();
        SetStudentData();
    }
    
    protected void SetStudentData()
    { 
        Age = MapperDate.CalcularEdad(Student.birthday) ?? 0;
        SelectActive = Student.isActive ? "true" : "false";
        Sex = Student.sex ? "true" : "false";
    }
    
    protected void GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value.ToString();
        setCurrentEnrollmentsByDegreeId(selectDegreeId);
    }

    private void setCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = Degrees.FirstOrDefault(e => e.degreeId == selectDegreeId);
        CurrentEnrollments = selectedDegree.enrollments;
    }

    protected void GetSection(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value.ToString();
        var enrollment = CurrentEnrollments.FirstOrDefault(e => e.enrollmentId == selectEnrollmentId);
        
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }
    protected void Save()
    {
        Student.isActive = SelectActive.ToLower() == "true";
        Student.sex = Sex.ToLower() == "true";
        Console.WriteLine(Student.isActive);
        Console.WriteLine(Student.sex);
    }
    
    protected byte[] pdfContent;
    protected async Task PrintSheetEnrollment(string studenId)
    {
        pdfContent = await Controller.GetPdfContent(studenId);
        await Navigator.InvokeModal("eval", "$('#ModalPdf').modal('show');");
    }
}