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
    private List<PartialEntity> OrderedPartials { get; set; } = new();
    private List<ConsolidatedStudentDto> ConsolidatedStudents { get; set; } = new();
    private bool _isLoading = true;
    private string? _errorMessage;
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            if (TeacherId == null || !IsGuide) return;
            
            _isLoading = true;
            _errorMessage = null;
            
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error al cargar datos: {ex.Message}";
            Console.WriteLine(_errorMessage);
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }

    private async Task LoadDataAsync()
    {
        Partials = await AddingStudentGradesController.GetPartialList();
        OrderedPartials = Partials.OrderBy(t => t.partialId).ToList();
        
        if (OrderedPartials.Count < 4)
        {
            throw new InvalidOperationException("Se requieren exactamente 4 parciales");
        }
        
        var tasks = new[]
        {
            GetPartialDataSafe(OrderedPartials[0].partialId),
            GetPartialDataSafe(OrderedPartials[1].partialId),
            GetPartialDataSafe(OrderedPartials[2].partialId),
            GetPartialDataSafe(OrderedPartials[3].partialId)
        };

        var results = await Task.WhenAll(tasks);
        ConsolidatedStudents = ConsolidateStudents(results[0], results[1], results[2], results[3]);
    }

    private async Task<List<TopStudentsDto>> GetPartialDataSafe(int partialId)
    {
        try
        {
            return await GeneratePerformanceReportBySection.GetTopStudents(TeacherId!, partialId) 
                   ?? new List<TopStudentsDto>();
        }
        catch
        {
            return new List<TopStudentsDto>();
        }
    }

    private List<ConsolidatedStudentDto> ConsolidateStudents(List<TopStudentsDto> iPartial, List<TopStudentsDto> iiPartial, List<TopStudentsDto> iiiPartial, List<TopStudentsDto> ivPartial)
    {
        var consolidated = new Dictionary<string, ConsolidatedStudentDto>();
        
        ProcessPartial(consolidated, iPartial, 0);
        ProcessPartial(consolidated, iiPartial, 1);
        ProcessPartial(consolidated, iiiPartial, 2);
        ProcessPartial(consolidated, ivPartial, 3);

        return consolidated.Values
            .OrderBy(s => s.FullName)
            .ToList();
    }

    private void ProcessPartial(Dictionary<string, ConsolidatedStudentDto> consolidated, List<TopStudentsDto> partialData, int partialIndex)
    {
        if (partialData == null) return;

        foreach (var student in partialData)
        {
            if (!consolidated.TryGetValue(student.studentId, out var consolidatedStudent))
            {
                consolidatedStudent = new ConsolidatedStudentDto
                {
                    StudentId = student.studentId,
                    FullName = student.fullName,
                    FinalGrade = student.finalGrade
                };
                consolidated[student.studentId] = consolidatedStudent;
            }

            // Tomar la primera calificación del parcial
            if (student.averageList.Count > 0)
            {
                var average = student.averageList[0];
                consolidatedStudent.PartialGrades[partialIndex] = new PartialGrade
                {
                    Grade = average.grade,
                    Label = average.label
                };
            }
        }
    }

    protected override bool IsLoading()
    {
        return _isLoading;
    }
}