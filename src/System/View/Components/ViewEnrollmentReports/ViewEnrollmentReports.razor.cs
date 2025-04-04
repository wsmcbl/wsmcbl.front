using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Components.ViewEnrollmentReports;

public partial class ViewEnrollmentReports : BaseView
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    private string? MyEnrollmentId { get; set; }
    private string? MyLabel { get; set; }
    public class Enrollment
    {
        public string EnrollmentId { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        
        public Enrollment() { }
        public Enrollment(string enrollmentId, string label)
        {
            EnrollmentId = enrollmentId;
            Label = label;
        }
    }
    private List<Enrollment> enrollments = new List<Enrollment>
    {
        new Enrollment("enr00026", "Octavo Grado B"),
        new Enrollment("enr00027", "Octavo Grado C"),
        new Enrollment("enr00031", "Segundo Grado B"),
        new Enrollment("enr00028", "Primer Grado A"),
        new Enrollment("enr00016", "Tercer Nivel B"),
        new Enrollment("enr00012", "Septimo Grado A"),
        new Enrollment("enr00018", "Tercer Grado B"),
        new Enrollment("enr00017", "Tercer Grado A"),
        new Enrollment("enr00034", "Cuarto Grado A"),
        new Enrollment("enr00030", "Segundo Grado A"),
        new Enrollment("enr00020", "Sexto Grado B"),
        new Enrollment("enr00019", "Sexto Grado A"),
        new Enrollment("enr00025", "Octavo Grado A"),
        new Enrollment("enr00023", "Undécimo Grado A"),
        new Enrollment("enr00024", "Undécimo Grado B"),
        new Enrollment("enr00021", "Quinto Grado A"),
        new Enrollment("enr00022", "Quinto Grado B"),
        new Enrollment("enr00015", "Tercer Nivel A"),
        new Enrollment("enr00032", "Noveno Grado A"),
        new Enrollment("enr00033", "Noveno Grado B"),
        new Enrollment("enr00029", "Primer Grado B"),
        new Enrollment("enr00010", "Décimo Grado A"),
        new Enrollment("enr00011", "Décimo Grado B"),
        new Enrollment("enr00035", "Cuarto Grado B"),
        new Enrollment("enr00013", "Septimo Grado B"),
        new Enrollment("enr00014", "Septimo Grado C")
    };

    private void SetEnrollmentId(string enrollmentId, string label)
    {
        MyEnrollmentId = enrollmentId;
        MyLabel = label;
    }
    
    private async Task DowloadReport()
    {
        await Controller.GetReportOfEnrollment(MyEnrollmentId!, 1, $"Reporte de {MyLabel}");
    }
}