using Newtonsoft.Json;

namespace wsmcbl.front.model.Academy.Input;

public class EnrollmentEntities
{
    [JsonProperty("idEnrollments")] 
    public string? IdEnrollments { get; set; }
    
    [JsonProperty("schoolYear")] 
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("fullName")] 
    public string? FullName { get; set; }
    
    [JsonProperty("sectionsQuantity")] 
    public int SectionsQuantity { get; set; }
    
    [JsonProperty("studentsQuantity")] 
    public int StudentsQuantity { get; set; }
    
    [JsonProperty("state")] 
    public bool State { get; set; }
    
    [JsonProperty("enrollments")] 
    public List<EnrollmentEntity> Enrollments { get; set; }
}