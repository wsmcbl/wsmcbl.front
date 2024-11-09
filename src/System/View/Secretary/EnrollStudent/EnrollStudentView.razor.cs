using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class EnrollStudentView : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = null!;
    [Inject] protected IEnrollStudentController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
   
    
    private StudentEntity? Student { get; set; }
    private string? enrollmentId { get; set; }
    private int discountId { get; set; }
    
    private int Age { get; set; }
    private string Sex;
    private string SelectActive;

    private List<DegreeBasicDto> Degrees = null!;
    private List<EnrollmentsBasicDto> CurrentEnrollments = [];
    private int CurrentEnrollmentCapacity, CurrentEnrollmentQuantity;
    private string EnrollmentIdSelected;
    
    private string imgSrc2;

    protected override async Task OnParametersSetAsync()
    {
        EnrollSheetPdf = [];
        Degrees = [];
        
        await LoadStudentInformation();
        SetStudentData();
    }
    
    private void SetDiscount(int value)
    {
        discountId = value;
    }

    private async Task LoadStudentInformation()
    {  
        var result = await Controller.GetInfoStudent(StudentId);
        Student = result.student;
        enrollmentId = result.enrollmentId;
        discountId = result.discountId;
        
        Degrees = await Controller.GetDegreeBasicList();
    }

    private void SetStudentData()
    { 
        Age = Student!.birthday.AgeCompute();
        SelectActive = Student.isActive ? "true" : "false";
        Sex = Student.sex ? "true" : "false";
        Student.parents[0].sex = false;
        Student.parents[1].sex = true;
    }
    
    private void GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value.ToString();
        setCurrentEnrollmentsByDegreeId(selectDegreeId);
    }
    
    private void setCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = Degrees.FirstOrDefault(e => e.degreeId == selectDegreeId);
        CurrentEnrollments = selectedDegree.enrollments;
    }

    private void GetSection(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value.ToString();
        var enrollment = CurrentEnrollments.FirstOrDefault(e => e.enrollmentId == selectEnrollmentId);
        
        EnrollmentIdSelected = enrollment.enrollmentId;
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }
    
    private async Task SaveEnrollment()
    {
        Student!.isActive = SelectActive.ToLower() == "true";
        Student.sex = Sex.ToLower() == "true";
        discountId = 2;

        if (Student.parents.Count != 0)
        {
            for (var index = 0; index < Student.parents.Count; index++)
            {
                if (!Student.parents[index].isValid())
                {
                    Student.parents.RemoveAt(index);
                }
            }
            
        }
        
        var response = await Controller.SaveEnrollment(Student, EnrollmentIdSelected, discountId);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Matrícula guardada exitosamente");
            await LoadStudentInformation();
            StateHasChanged();
        }
    }
    
    private byte[] EnrollSheetPdf { get; set; }
    private async Task PrintEnrollSheet(string studentId)
    {
        EnrollSheetPdf = await Controller.GetEnrollSheetPdf(studentId);

        if (EnrollSheetPdf.Length == 0)
        {
            return;
        }

        await Navigator.ShowPdfModal();
    }
}