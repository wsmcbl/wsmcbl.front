using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public partial class StudentsView : BaseView
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private UpdateStudentController Controller { get; set; } = null!;
    
    private UserEntity User { get; set; } = new();
    private StudentEntity? student { get; set; }
    private bool generateToken { get; set; } = false;
    private bool EditMode { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        User = await LoginController.getUserById();
        GetEditMode();
        if (User.permissionList.Contains(4))
        {
            student = await Controller.GetStudentById(StudentId);
            return;
        }
        
        var response = await Notificator.ShowAlertQuestionOnlyYes("Error",
            "No tienes los permisos suficiente en este momento intentelo mas tarde",
            "Volver");
        
        if (response)
        { 
            Navigator.ToPage("/academy/enrollments/guide");
        }
    }
    
    private void GetEditMode()
    {
        if (User.permissionList.Contains(3))
        {
            EditMode = true;
        }
    }
    
    private async Task UpdateStudent()
    {
        if (student == null)
        {
            throw new InternalException("StudentEntity must be not null.");
        }
        
        student.studentId = StudentId;
        var response = await Controller.UpdateStudentData(student, generateToken);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado el estudiante correctamente.");
            return;
        }
        
        await Notificator.ShowError( "Hubo un fallo al actualizar el estudiante.");
    }

    protected override bool IsLoading()
    {
        return student == null;
    }
    
}