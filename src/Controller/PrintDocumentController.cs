using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.MonthToViewGrade;

namespace wsmcbl.src.Controller;

public class PrintDocumentController : BaseController
{
    public PrintDocumentController(ApiConsumerFactory apiFactory) : base(apiFactory, string.Empty)
    {
    }

    public async Task<byte[]> GetOfficialEnrollmentListDocument()
    {
        return await apiFactory
            .WithNotificator.GetByteFileAsync(Modules.Secretary, "degrees/export");
    }

    public async Task<List<MediaDto>> GetAllMedia()
    {
        var Default = new List<MediaDto>();
        return await apiFactory.WithNotificator.GetAsync(Modules.Resources, "medias", Default);
    }
    
    public async Task<bool> UpdateMedia(int month)
    {
        var Default = new List<MediaDto>();
        return await apiFactory.WithNotificator.PutAsync(Modules.Resources, $"media/report-card-month?month={month}", Default);
    }
    
}