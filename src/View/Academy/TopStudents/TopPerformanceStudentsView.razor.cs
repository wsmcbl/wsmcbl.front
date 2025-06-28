using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.TopStudents;

public partial class TopPerformanceStudentsView : BaseView
{
    [Inject] private GeneratePerformanceReportBySection GeneratePerformanceReportBySection { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;

    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public bool IsGuide { get; set; }

    private List<PartialEntity> Partials { get; set; } = new();
    private List<TopStudentsDto>? Students { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        if (TeacherId != null && IsGuide)
        {
            Partials = await AddingStudentGradesController.GetPartialList();
            Students = await GeneratePerformanceReportBySection.GetTopStudents(TeacherId);
        }
    }


    protected override bool IsLoading()
    {
        // Retorna true (cargando) si:
        // 1. NO es guía (IsGuide == false) → muestra siempre cargando
        // O
        // 2. ES guía PERO los datos NO están cargados aún
        return !IsGuide || (IsGuide && (Partials.Count == 0 || Students == null));
    }
}