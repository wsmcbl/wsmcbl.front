using wsmcbl.front.Model.Secretary;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.View.Secretary.SchoolYears;

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