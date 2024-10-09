using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;
using TariffCreateNewSchoolYearDto = wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear.TariffCreateNewSchoolYearDto;
namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperSchoolYear
{
    public static CreateSchoolYearDto MapToNewSchoolYearDto (SchoolYearEntity schoolYearEntity)
    {
        if (schoolYearEntity == null) return null;
        
        return new CreateSchoolYearDto
        {
            degrees = schoolYearEntity.Degrees?.Select(MapToGradeCreateNewSchoolYearDto).ToList(),
            tariffs = schoolYearEntity.Tariffs?.Select(MapToTariffCreateNewSchoolYearDto).ToList()
        };
    }
    
    private static GradeCreateNewSchoolYearDto MapToGradeCreateNewSchoolYearDto(GradeDto gradeDto)
    {
        if (gradeDto == null) return null;

        return new GradeCreateNewSchoolYearDto
        {
            label = gradeDto.label,
            schoolYear = gradeDto.schoolYear,
            modality = gradeDto.modality,
            subjects = gradeDto.subjects?.Select(SubjectDto.MapToSubjectsCreateNewSchoolYearDto).ToList()
        };
    }
    
    private static TariffCreateNewSchoolYearDto MapToTariffCreateNewSchoolYearDto(SchoolYearTariffs tariffs)
    {
        if (tariffs == null) return null;

        return new TariffCreateNewSchoolYearDto
        {
            schoolYear = tariffs.SchoolYear,
            concept = tariffs.Concept,
            amount = tariffs.Amount,
            DueDateEntity = MapperDate.ConvertDateOnlyToDate(tariffs.OnlyDate),
            type = tariffs.Type,
            modality = tariffs.Modality
        };
    }
    
}