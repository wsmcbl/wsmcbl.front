using CurrieTechnologies.Razor.SweetAlert2;

namespace wsmcbl.src.Utilities;

public class Notificator
{
    private readonly SweetAlertService Service;

    public Notificator(SweetAlertService service)
    {
        Service = service;
    }

    public async Task ShowSuccess(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{ Title = title, Text = text, Icon = SweetAlertIcon.Success});
    }
    
    public async Task ShowInformation(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{ Title = title, Text = text, Icon = SweetAlertIcon.Info});
    }
    
    public async Task ShowError(string title, string text, string footer)
    {
        await Service.FireAsync(new SweetAlertOptions 
        {
            Title = title,
            Text = text,
            Footer  = footer,
            Icon = SweetAlertIcon.Error
        });
    }
    
    public async Task ShowError(string title, string text)
    {
        await ShowError(title, text, string.Empty);
    }

    public async Task ShowError(string content)
    {
        await ShowError("Ocurrió un error.", content);
    }
    
    public async Task ShowWarning(string title, string text)
    {
        await Service.FireAsync(new SweetAlertOptions{Title = title, Text = text, Icon= SweetAlertIcon.Warning, ShowCancelButton = true });
    }

    public async Task<bool> ShowConfirmationQuestion(string title, string content, (string confirmText, string denyText) options)
    {
        var result = await Service.FireAsync(new SweetAlertOptions
        {
            Icon = SweetAlertIcon.Success,
            Title = title,
            Text = content,
            ShowDenyButton = true,
            ConfirmButtonText = options.confirmText,
            DenyButtonText = options.denyText
        });

        return result.IsConfirmed;
    }
    
    public async Task<bool> ShowAlertQuestion(string title, string content, (string confirmText, string denyText) options)
    {
        var result = await Service.FireAsync(new SweetAlertOptions
        {
            Icon = SweetAlertIcon.Warning,
            Title = title,
            Text = content,
            ShowDenyButton = true,
            ConfirmButtonText = options.confirmText,
            DenyButtonText = options.denyText
        });

        return result.IsConfirmed;
    }
}