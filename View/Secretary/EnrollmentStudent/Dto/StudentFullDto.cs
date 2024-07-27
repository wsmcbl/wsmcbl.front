using Newtonsoft.Json;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.View.Secretary.EnrollmentStudent.Dto;

public class StudentFullDto
{
    [JsonProperty("studentId")]
    public string StudentId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("secondName")]
    public string SecondName { get; set; }
    
    [JsonProperty("surname")]
    public string Surname { get; set; }
    
    [JsonProperty("secondSurname")]
    public string SecondSurname { get; set; }
    
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
    
    [JsonProperty("sex")]
    public bool Sex { get; set; }
    
    [JsonProperty("birthday")]
    public Date birthday { get; set; }
    
    [JsonProperty("religion")]
    public string Religion { get; set; }
    
    [JsonProperty("diseases")]
    public string Diseases { get; set; }
    
    [JsonProperty("parents")]
    public List<Parent> Parents { get; set; }
    
    [JsonProperty("tutor")]
    public Tutor Tutor { get; set; }
    
    [JsonProperty("measurements")]
    public Measurements Measurements { get; set; }
    
    [JsonProperty("studentId")]
    public File File { get; set; }
}




public class Parent
{
    [JsonProperty("parentId")]
    public string parentId { get; set; }
    
    [JsonProperty("sex")]
    public bool Sex { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("address")]
    public string Address { get; set; }
    
    [JsonProperty("idCard")]
    public string IdCard { get; set; }
    
    [JsonProperty("phone")]
    public string Phone { get; set; }
    
    [JsonProperty("occupation")]
    public string Occupation { get; set; }
}

public class Tutor
{
    [JsonProperty("tutorId")]
    public string TutorId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("phone")]
    public string Phone { get; set; }
}

public class Measurements
{
    [JsonProperty("measurementId")]
    public string MeasurementId { get; set; }
    
    [JsonProperty("height")]
    public int Height { get; set; }
    
    [JsonProperty("weight")]
    public int Weight { get; set; }
}

public class File
{
    [JsonProperty("fileId")]
    public string FileId { get; set; }
    
    [JsonProperty("transferSheet")]
    public bool TransferSheet { get; set; }
    
    [JsonProperty("birthDocument")]
    public bool BirthDocument { get; set; }
    
    [JsonProperty("parentIdentifier")]
    public bool ParentIdentifier { get; set; }
    
    [JsonProperty("updatedGradeReport")]
    public bool UpdatedGradeReport { get; set; }
    
    [JsonProperty("conductDocument")]
    public bool ConductDocument { get; set; }
    
    [JsonProperty("financialSolvency")]
    public bool FinancialSolvency { get; set; }
}