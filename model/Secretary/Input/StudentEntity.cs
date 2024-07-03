using Newtonsoft.Json;

namespace wsmcbl.front.Models.Secretary.Input;

public class StudentEntity
{   
    [JsonProperty("studentId")]
    public string StudentId { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("secondName")]
    public string SecondName { get; set; } = null!;

    [JsonProperty("surname")]
    public string Surname { get; set; } = null!;

    [JsonProperty("secondSurname")]
    public string SecondSurname { get; set; } = null!;

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;

    [JsonProperty("tutor")]
    public string Tutor { get; set; } = null!;

    [JsonProperty("sex")]
    public bool Sex { get; set; }

    [JsonProperty("birthday")]
    public DateOnly Birthday { get; set; }

    [JsonProperty("enrollmentLabel")]
    public string EnrollmentLabel { get; set; } = null!;
}