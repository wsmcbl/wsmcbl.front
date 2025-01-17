using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class EnrollStudentView : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = null!;
    [Inject] protected EnrollStudentController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    
     
    private StudentEntity? Student { get; set; }
    private int DiscountId { get; set; }
    private List<DegreeBasicDto>? Degrees { get; set; }
    
    private int Age { get; set; }
    private string? EnrollmentIdSelected { get; set; }
    private bool IsStudentsEnrollment { get; set; }
    private bool IsRepeating { get; set; }
    private bool isLoading = true;
    
    protected override async Task OnParametersSetAsync()
    {
        
        if (isLoading)
        {
            EnrollSheetPdf = [];
            await LoadStudentInformation();
            isLoading = false;
        }
        
    }
    
    private async Task LoadStudentInformation()
    {  
        var result = await Controller.GetInfoStudent(StudentId);
        Degrees = await Controller.GetDegreeBasicList();
        
        IsStudentsEnrollment = result.enrollmentId != null;
        Student = result.student;
        DiscountId = result.discountId;
        
        if (!Student.parents!.Any())
        {
            Student.parents = [];
        }
    }
    
    private void OnRepeatSelectionChanged(bool value)
    {
        IsRepeating = value;
    }
    
    private async Task SaveEnrollment()
    {
        if (Student != null && !Student.IsStudentValid())
        {
           await Notificator.ShowInformation("Los campos marcados con (*) son obligatorios.");
            return;
        }

        if (!IsParentsValid())
        {
            await Notificator.ShowInformation("Los campos de un tutor deben ser ingresados, o bien dejar en blanco.");
            return;
        }
        
        if (!Student!.IsTutorValid())
        {
            await Notificator.ShowInformation("Por favor, ingrese todos los campos del tutor.");
            return;
        }

        if (!Student.IsMeasurementsValid())
        {
            await Notificator.ShowInformation("Ingrese un peso dentro de los margenes permitidos.");
            return;
        }

        if (string.IsNullOrWhiteSpace(EnrollmentIdSelected) || EnrollmentIdSelected == "No asignado")
        {
            await Notificator.ShowInformation("Advertencia",
                "Por favor seleccione un grado y una sección para el estudiante.");
            return;
        }
        
        var response = await Controller.SaveEnrollment(Student, EnrollmentIdSelected, DiscountId, IsRepeating);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha registrado la matrícula correctamente.");
            await LoadStudentInformation();
            StateHasChanged();
        }
    }

    private bool IsParentsValid()
    {
        if (Student!.parents!.Count == 0)
        {
            return true;
        }
        
        for (var index = Student.parents.Count - 1; index >= 0; index--)
        {
            if (Student.parents[index].isTutorPartiallyFilled())
            {
                return false;
            }
             
            if (Student.parents[index].isTutorEmpty())
            {
                Student.parents[index].init();
            }
        }

        return true;
    }

    private byte[]? EnrollSheetPdf { get; set; }
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