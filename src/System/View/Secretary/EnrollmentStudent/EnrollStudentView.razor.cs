using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

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
    protected string EnrollmentIdSelectec;

    protected override async Task OnParametersSetAsync()
    {
        await loadStudentInformation();
        SetStudentData();
    }

    private async Task loadStudentInformation()
    {  
        Student = await Controller.GetInfoStudent(StudentId);
        Degrees = await Controller.GetDegreeBasicList();
    }

    protected void SetStudentData()
    { 
        Age = MapperDate.CalcularEdad(Student.birthday) ?? 0;
        SelectActive = Student.isActive ? "true" : "false";
        Sex = Student.sex ? "true" : "false";
        Student.parents[0].sex = false;
        Student.parents[1].sex = true;
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
        
        EnrollmentIdSelectec = enrollment.enrollmentId;
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }
    
    protected async Task SaveEnrollment()
    {
        Student.isActive = SelectActive.ToLower() == "true";
        Student.sex = Sex.ToLower() == "true";

        for (var index = 0; index < 2; index++)
        {
            if (!Student.parents[index].isValid())
            {
                Student.parents.RemoveAt(index);
            }
        }
        
        var response = await Controller.SaveEnrollment(Student, EnrollmentIdSelectec);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Matricula guardada exitosamente");
            await loadStudentInformation();
            StateHasChanged();
        }
    }
    
    protected byte[] pdfContent;
    protected async Task PrintSheetEnrollment(string studenId)
    {
        pdfContent = await Controller.GetPdfContent(studenId);
        await Navigator.ShowModal("ModalPdf");
    }
}