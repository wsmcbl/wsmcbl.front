using DocumentFormat.OpenXml.Office2010.ExcelAc;
using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Components.ViewEnrollmentReports;

public class CombinedDegreeDto
{
    public string? DegreeId { get; set; }
    public string Label { get; set; } = "N/A";
    public string SchoolYear { get; set; } = "N/A";
    public int Position { get; set; }
    public string? EducationalLevel { get; set; }
    
    // Datos adicionales de Enrollment
    public List<EnrollmentListDto> EnrollmentList { get; set; } = [];
}