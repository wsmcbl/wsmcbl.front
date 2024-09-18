using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent;

public class EnrollStudent : ComponentBase
{
    [Parameter] public string StudentId { get; set; }
    [Inject] protected IEnrollStudentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    [Inject] protected Navigator Navigator { get; set; }

    protected StudentFullDto Student;
    protected int Age;
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
        SelectActive = Student.isActive ? "true" : "false";
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
        Console.WriteLine(Student);
    }
    
    protected byte[] pdfContent;
    protected async Task PrintSheetEnrollment(string studenId)
    {
        pdfContent = await Controller.GetPdfContent(studenId);
        await Navigator.InvokeModal("eval", "$('#ModalPdf').modal('show');");
    }
}