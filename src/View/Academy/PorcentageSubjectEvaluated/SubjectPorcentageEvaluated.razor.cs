using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Academy.EnrollmentListByTeacher;
using wsmcbl.src.View.Academy.SubjectByTeacher;

namespace wsmcbl.src.View.Academy.PorcentageSubjectEvaluated;

public partial class SubjectPorcentageEvaluated : ComponentBase
{
    [Inject] private TeacherDashboardController Controller { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;
    [Parameter] public string? TeacherId { get; set; }
    private List<SubjectStats> Subjects = new();
    private string searchTerm = string.Empty;
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = [];
    private List<SubjectByTeacherDto> SubjectListForTeacher { get; set; } = [];

    
    protected override async Task OnParametersSetAsync()
    {
        if (TeacherId != null)
        {
            Subjects = await Controller.GetSubjectEvaluated(TeacherId);
            EnrollmentList = await AddingStudentGradesController.GetEnrollmentList(TeacherId);
            SubjectListForTeacher = await Controller.GetSubjectTeacherListById(TeacherId);

        }
    }
    
    //Mejorar el buscador para poder buscar por nombre de la asignatura. (Pendiente)
    private List<SubjectStats> FilteredSubjects => 
        string.IsNullOrWhiteSpace(SearchTerm)
            ? Subjects
            : Subjects.Where(s => s.subjectId.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || 
                                  s.enrollmentId.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

    private int GetPercentageEvaluated(SubjectStats subject)
    {
        if (subject.studentsCount.total == 0) return 100;
        return (int)((subject.studentsCount.total - subject.studentsNotEvaluated.total) * 100 / subject.studentsCount.total);
    }

    private string SearchTerm
    {
        get => searchTerm;
        set
        {
            searchTerm = value;
            StateHasChanged();
        }
    }
    
    private void ClearSearch()
    {
        SearchTerm = string.Empty;
    }


}