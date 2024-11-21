using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public static class MapperDto
{
    public static List<CreateEnrollmentsDto> MapToListDto(this DegreeEntity value)
    {
        return value.EnrollmentList?.Select(e => new CreateEnrollmentsDto(e)).ToList()!; 
    }

    public static List<SubjectTeacherDto> MapToListDto(this EnrollmentEntity value)
    {
        return value.SubjectTeacherList
            .Select(st => new SubjectTeacherDto(st.subject, st.teacher))
            .ToList();
    }
}