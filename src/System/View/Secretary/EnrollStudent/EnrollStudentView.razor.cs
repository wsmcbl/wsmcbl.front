using Microsoft.AspNetCore.Components;
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
    private List<DegreeBasicDto>? Degrees { get; set; }
    private List<EnrollmentsBasicDto>? CurrentEnrollments { get; set; }
    
    
    private int discountId { get; set; }
    private int Age { get; set; }
    private string Sex { get; set; }
    private string SelectActive { get; set; }
    private int CurrentEnrollmentCapacity { get; set; }
    private int CurrentEnrollmentQuantity { get; set; }
    private string EnrollmentIdSelected { get; set; }
    private bool IsStudentsEnrollment { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        EnrollSheetPdf = [];
        Degrees = await Controller.GetDegreeBasicList();
        await LoadStudentInformation();
        SetStudentData();
    }
    
    private async Task LoadStudentInformation()
    {  
        var result = await Controller.GetInfoStudent(StudentId);
        IsStudentsEnrollment = result.enrollmentId != null;
        if (Degrees.Any())
        {
            CurrentEnrollments = Degrees
                .Where(t => t.enrollments != null && t.enrollments.Any())
                .Select(t => t.enrollments)
                .FirstOrDefault(); 
            EnrollmentIdSelected  = CurrentEnrollments.FirstOrDefault()?.enrollmentId ?? "No asignado";
        }
        else
        {
            CurrentEnrollments = new List<EnrollmentsBasicDto> { new() };       
        }
        
        Student = result.student;
        discountId = result.discountId;
    }

    private void SetStudentData()
    { 
        Age = Student!.birthday.AgeCompute();
        SelectActive = Student.isActive ? "true" : "false";
        Sex = Student.sex ? "true" : "false";
        Student.parents[0].sex = false;
        Student.parents[1].sex = true;
    }
    
    private void SetDiscount(int value)
    {
        discountId = value;
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
        
        if (Student.parents.Count != 0)
        {
            for (var index = Student.parents.Count - 1; index >= 0; index--)
            {
                if (Student.parents[index].isTutorPartiallyFilled())
                {
                    await Notificator.ShowInformation("Advertencia",
                        "Todos los campos de un tutor deben ser rellenados, de lo contrario dejar en blanco");
                    return;
                }
             
                if (Student.parents[index].isTutorEmpty())
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