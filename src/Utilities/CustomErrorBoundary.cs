using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace wsmcbl.src.Utilities;

public class CustomErrorBoundary : ErrorBoundary
{
    [Inject] protected SweetAlertService? Service { get; set; }

    protected override async Task OnErrorAsync(Exception exception)
    {
        var title = "Surgió un error.";
        var text = exception.Message;
        
        if (exception is InternalException appException)
        {
            title = appException.Title;
            text = appException.Content;
        }
        
        await Service!.FireAsync(
            new SweetAlertOptions
            {
                Title = title,
                Text = text,
                Icon = SweetAlertIcon.Error
            });
    }
}