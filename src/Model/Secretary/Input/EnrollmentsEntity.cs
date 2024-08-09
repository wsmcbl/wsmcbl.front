using Newtonsoft.Json;

namespace wsmcbl.src.Model.Secretary.Input;

public class EnrollmentsEntity
{
    [JsonProperty("enrollmentId")] public string EnrollmentId { get; set; }

    [JsonProperty("label")] public string Label { get; set; } = null!;

    [JsonProperty("section")] public string Section { get; set; }

    [JsonProperty("capacity")] public int Capacity { get; set; }

    [JsonProperty("quantity")] public int Quantity { get; set; }

    [JsonProperty("students")] public List<StudentEntity> Students { get; set; } = null!;

    [JsonProperty("teacherGuide")] public TeacherEntity TeacherGuide { get; set; } = null!;

    [JsonProperty("subjects")] public List<SubjectEntity> Subjects { get; set; } = null!;
}