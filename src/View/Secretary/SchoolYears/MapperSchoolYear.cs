using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperSchoolYear
{
    public static CreateSchoolYearDto MapToNewSchoolYearDto (SchoolYearEntity schoolYearEntity)
    {
        return new CreateSchoolYearDto()
        {
            degrees = schoolYearEntity.Degrees,
            tariffs = schoolYearEntity.Tariffs
        };
    }
}