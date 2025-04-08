using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.Statistics;
using wsmcbl.src.View.Management.PartialsGradeRecording;

namespace wsmcbl.src.Controller;

public class GenerateEvaluationStatsBySectionController : BaseController
{
    public GenerateEvaluationStatsBySectionController(ApiConsumerFactory apiFactory) : base(apiFactory, "teachers")
    {
    }
    
    public async Task<EvaluationSumaryDto?> GetStatsSummary(string teacherId, int partialId)
    {
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy, path+$"/{teacherId}/enrollments/guide/stats/evaluated/summary?partialId={partialId}", new EvaluationSumaryDto());
    }
}