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
    private int discountId { get; set; }
    private List<DegreeBasicDto>? Degrees { get; set; }
    
    private int Age { get; set; }
    private string Sex { get; set; }
    private string SelectActive { get; set; }
    private string EnrollmentIdSelected { get; set; }
    private bool IsStudentsEnrollment { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        EnrollSheetPdf = [];
        await LoadStudentInformation();
        SetStudentData();
    }
    
    private async Task LoadStudentInformation()
    {  
        var result = await Controller.GetInfoStudent(StudentId);
        Degrees = await Controller.GetDegreeBasicList();
        IsStudentsEnrollment = result.enrollmentId != null;
        Student = result.student;
        discountId = result.discountId;
    }

    private void SetStudentData()
    { 
        if (Student != null)
        {
            Age = Student.birthday.AgeCompute();
            SelectActive = Student.isActive ? "true" : "false";
            Sex = Student.sex ? "true" : "false";
        }
        
        if (Student?.parents != null && Student.parents.Count >= 2)
        {
            Student.parents[0].sex = false;
            Student.parents[1].sex = true;
        }
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
                    Student.parents[index].SetDefault();
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