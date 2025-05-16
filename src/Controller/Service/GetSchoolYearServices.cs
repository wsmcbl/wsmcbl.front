namespace wsmcbl.src.Controller.Service;

public class GetSchoolYearServices
{
    private readonly CreateSchoolyearController _controller;

    public GetSchoolYearServices(CreateSchoolyearController controller)
    {
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    public async Task<Dictionary<string, string>> GetSchoolYearLabelsBatch(IEnumerable<string> ids)
    {
        var allSchoolYears = await _controller.GetSchoolYearList();
        return allSchoolYears
            .Where(x => ids.Contains(x.schoolyearId))
            .ToDictionary(x => x.schoolyearId, x => x.label);
    }
    
    public async Task<string> GetSchoolYearLabel(string schoolYearId)
    {
        if (string.IsNullOrWhiteSpace(schoolYearId))
        {
            throw new ArgumentException("School year ID cannot be null or empty", nameof(schoolYearId));
        }

        var schoolyearList = await _controller.GetSchoolYearList();
        
        var schoolYear = schoolyearList.FirstOrDefault(t => t.schoolyearId == schoolYearId);
        
        if (schoolYear == null)
        {
            throw new KeyNotFoundException($"School year with ID {schoolYearId} not found");
        }

        return schoolYear.label;
    }
}