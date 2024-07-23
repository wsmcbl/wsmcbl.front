using CurrieTechnologies.Razor.SweetAlert2;

namespace wsmcbl.front.View.Shared;

public class AlertService
{
    private SweetAlertService service;

    public AlertService(SweetAlertService service)
    {
        this.service = service;
    }

    public async Task AlertSuccess(string title, string text)
    {
        await service.FireAsync(new SweetAlertOptions{ Title = title, Text = text, Icon = SweetAlertIcon.Success});
    }
    
    public async Task AlertError(string title, string text)
    {
        await service.FireAsync(new SweetAlertOptions{ Title = title, Text = text,  Icon = SweetAlertIcon.Error});
    }
    
    public async Task AlertWarning(string title)
    {
        await service.FireAsync(new SweetAlertOptions{Title = title, Icon = SweetAlertIcon.Warning, ShowCancelButton = true });
    }
}