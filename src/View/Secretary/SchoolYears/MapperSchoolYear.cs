using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperSchoolYear
{
    public static NewSchoolYearDto MapToNewSchoolYearDto (SchoolYearEntity schoolYearEntity)
    {
        return new NewSchoolYearDto()
        {
            Grades = schoolYearEntity.Degrees,
            Tariffs = schoolYearEntity.Tariffs
        };
    }
}