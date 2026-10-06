using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Management.ReportUserCalification;
using StudentEntity = wsmcbl.src.Model.Secretary.StudentEntity;

namespace wsmcbl.src.View.Secretary.AddGradeToStudentReturning;

public partial class AddGradeStudentReturning : BaseView
{
    [Parameter] public string StudentId { get; set; } = string.Empty;
    [Inject] protected EnrollStudentController Controller { get; set; } = null!;
    [Inject] protected AddingStudentGradesController GradeController { get; set; } = null!;
    [Inject] protected AddStudentReturningController ReturningController { get; set; } = null!;
    [Inject] protected ViewPrincipalDashboardController SubjectController { get; set; } = null!;
    [Inject] protected UpdateOfficialEnrollmentController EnrollmentController { get; set; } = null!;
    [Inject] protected Notificator notificator { get; set; } = null!;

    private StudentEntity FullStudent { get; set; } = new();
    private List<DetailForSubjectAddGradeReturning> DetailSubject { get; set; } = new();
    private GradeDto Grade { get; set; } = new();
    private List<PartialEntity> PartialList = [];
    private List<SubjectsDto> SubjectCatalog { get; set; } = [];
    private List<TeacherEntity> TeachersCatalog { get; set; } = [];

    private int? PartialId { get; set; }
    private string EnrollmentId { get; set; } = string.Empty;

    // Banderas de estado para evitar bloqueos y manejar errores
    private bool loading = true;
    private bool isEnrolled = true;

    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrWhiteSpace(StudentId))
        {
            loading = true;
            try
            {
                var result = await Controller.GetStudentById(StudentId);
                
                if (!string.IsNullOrEmpty(result.enrollmentId))
                {
                    EnrollmentId = result.enrollmentId;
                    FullStudent = result.student;
                    isEnrolled = true;
                    await GetPartialList();
                }
                else
                {
                    isEnrolled = false;
                }
            }
            catch (Exception)
            {
                // Absorbe el 409 u otros errores de falta de matrícula/solvencia
                isEnrolled = false;
                EnrollmentId = string.Empty;
            }
            finally
            {
                loading = false;
            }
        }
    }
    
    private void OnCargarDatos()
    {
        Grade.grades = DetailSubject.Select(item => new GradeDetailDto
        {
            subjectPartialId = item.subjectPartialId,
            grade = 0,
            conductgrade = 0
        }).ToList();
    }

    private async Task GetPartialList()
    {
        var partial = await GradeController.GetPartialList();
        PartialList = partial.OrderBy(p => p.partialId).ToList();
    }
    
    private async Task Save()
    {
        if (!isEnrolled) return;

        var des = await notificator.ShowAlertQuestion("Advertencia",
            "Al realizar esta acción se actualizaran las calificaciones existentes, o se agregaran si no existen. ¿Estas seguro de continuar?",
            ("SI", "NO"));
        if (!des) { return; }
        
        if (Grade.grades.Count == 0)
        {
            await notificator.ShowWarning("Advertencia","No hay notas para guardar.");
            return;
        }
        
        bool invalidGrades = Grade.grades.Any(g => 
            g.grade < 0 || g.grade > 100 || 
            g.conductgrade < 0 || g.conductgrade > 100
        );

        if (invalidGrades)
        {
            await notificator.ShowWarning("Advertencia","Todas las calificaciones y notas de conducta deben estar entre 0 y 100.");
            return;
        }
        
        Grade.studentId = StudentId;
        var response = await ReturningController.Save(Grade);
        if (response)
        {
            await notificator.ShowSuccess($"Hemos actualizado correctamente las calificaciones del estudiante {FullStudent.FullName()}");
            return;
        }
        await notificator.ShowError($"No hemos podido guardar o actualiar las calificaciones para el estudiante {FullStudent.FullName()}");
    }
    
    private void SanitizeIndex(int index, bool isConduct)
    {
        if (isConduct)
        {
            Grade.grades[index].conductgrade = Math.Clamp(Grade.grades[index].conductgrade, 0, 100);
        }
        else
        {
            Grade.grades[index].grade = Math.Clamp(Grade.grades[index].grade, 0, 100);
        }
    }
    
    private async Task GetGradeInfo()
    {
        if (PartialId.HasValue && !string.IsNullOrEmpty(EnrollmentId))
        {
            DetailSubject = await ReturningController.GetSubjectInfo(EnrollmentId, PartialId);
            SubjectCatalog = await SubjectController.GetSubjectOfSchoolYear();
            TeachersCatalog = await EnrollmentController.GetActiveTeacherList();
            OnCargarDatos();
        }
    } 

    protected override bool IsLoading() => loading;

    private void OnCheckboxChange(ChangeEventArgs e, int partialId)
    {
        bool isChecked = (bool)(e.Value ?? false);

        if (isChecked)
        {
            PartialId = partialId;
        }
        else if (PartialId == partialId)
        {
            PartialId = null; 
        }
    }
}