using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class EnrollmentSubjectListDto
{
    public List<EnrollmentEntity> enrollmentList { get; set; } = null!;
    public List<SubjectEntity> subjectList { get; set; } = null!;

    public void setSubjectListInEnrollmentList()
    {
        foreach (var item in enrollmentList)
        {
            item.updateSubjectList(subjectList);
        }
    }
}