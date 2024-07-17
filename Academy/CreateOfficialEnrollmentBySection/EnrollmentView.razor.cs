using wsmcbl.front.model.Secretary.Input;


namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public  class EnrollmentView_razor : EnrollmentBrich_razor
{
    protected List<Enrollments>? EnrollmentList { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        EnrollmentList = await GetEnrollments();
    }
    private async Task<List<Enrollments>?> GetEnrollments()
    {

        var enrollments = new Enrollments()
        {
            EnrollmentId = "001",
            SchoolYear = "2023",
            Label = "Primer Grado",
            Section = "A",
            Capacity = 50,
            Quantity = 2,
            GradeId = 1,
            State = false
            
            
        };
        
        var enrollments2 = new Enrollments()
        {
            EnrollmentId = "002",
            SchoolYear = "2023",
            Label = "Primer Grado",
            Section = "B",
            Capacity = 0,
            Quantity = 0,
            GradeId = 1,
            State = true
        };

        return new List<Enrollments> { enrollments, enrollments2};
    }
    
    
}