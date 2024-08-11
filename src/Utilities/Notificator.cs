using CurrieTechnologies.Razor.SweetAlert2;

namespace wsmcbl.src.Utilities;

public class Notificator
{
    private readonly SweetAlertService Service;

    public Notificator(SweetAlertService service)
    {
        Service = service;
    }

    public async Task AlertSuccess(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{ Title = title, Text = text, Icon = SweetAlertIcon.Success});
    }
    
    public async Task AlertInformation(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{ Title = title, Text = text, Icon = SweetAlertIcon.Info});
    }
    
    public async Task AlertError(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{ Title = title, Text = text,  Icon = SweetAlertIcon.Error});
    }

    public async Task AlertError(string content)
    {
        await AlertError("Ocurrió un error.", content);
    }
    
    public async Task AlertWarning(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{Title = title, Text = text, Icon= SweetAlertIcon.Warning, ShowCancelButton = true });
    }

    public async Task<bool> AlertQuestionTwoOptions(string title, string content, (string confirmText, string denyText) options)
    {
        var result = await Service.FireAsync(new SweetAlertOptions
        {
            Title = title,
            Text = content,
            ShowDenyButton = true,
            ConfirmButtonText = options.confirmText,
            DenyButtonText = options.denyText
        });

        return result.IsConfirmed;
    }
}