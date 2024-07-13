using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.model.Academy.Input;
using wsmcbl.front.model.Secretary.Input;


namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public  class CreateOfficialEnrollmentBySectionRazor : ComponentBase
{
    protected List<EnrollmentEntities>? EnrollmentList { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        EnrollmentList = await GetEnrollments();
    }
    
    private async Task<List<EnrollmentEntities>?> GetEnrollments()
    {

        var enrollments = new EnrollmentEntities()
        {
            IdEnrollments = "001",
            SchoolYear = "2023",
            FullName = "Primer Grado",
            SectionsQuantity = 2,
            StudentsQuantity = 60,
            State = false,
            Enrollments = null,
        };

        return new List<EnrollmentEntities> { enrollments };
        
    }
}