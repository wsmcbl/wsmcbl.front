using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class CreateEnrollmentsDto
{
    public string enrollmentId { get; set; }
    public string teacherId { get; set; }
    public string section { get; set; }
    public string label { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public List<ListSubjecDto> subjects { get; set; }
    
    public class ListSubjecDto
    {
        public string subjectId { get; set; }
        public string teacherId { get; set; }
    }
    
    
    public static List<CreateEnrollmentsDto> MaptoCreateEnrollmentsDto(DegreeEntity degree)
    {
        return degree.EnrollmentList?.Select(enrollment => new CreateEnrollmentsDto
        {
            enrollmentId = enrollment.enrollmentId,
            teacherId = enrollment.teacherId ?? string.Empty,
            section = enrollment.Section,
            label = enrollment.Label,
            capacity = enrollment.Capacity,
            quantity = enrollment.Quantity,
            subjects = enrollment.SubjectTeacherList.Select(st => new CreateEnrollmentsDto.ListSubjecDto
            {
                subjectId = st.subject.SubjectId ?? string.Empty, 
                teacherId = st.teacher?.teacherId ?? string.Empty
            }).ToList()
        }).ToList() ?? new List<CreateEnrollmentsDto>(); 
    }
}