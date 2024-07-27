using wsmcbl.front.Model.Secretary.Input;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public static class MapperSchoolYear
{
    public static NewSchoolYearDto MapToNewSchoolYearDto (SchoolYearEntity schoolYearEntity)
    {
        return new NewSchoolYearDto()
        {
            Grades = schoolYearEntity.Grades,
            Tariffs = schoolYearEntity.Tariffs
        };
    }
}