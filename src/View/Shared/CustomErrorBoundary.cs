using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace wsmcbl.src.View.Shared;

public class CustomErrorBoundary : ErrorBoundary
{
    [Inject] protected SweetAlertService service { get; set; }

    protected override async Task OnErrorAsync(Exception exception)
    {
        await service.FireAsync(new SweetAlertOptions
            { Title = "Un error", Text = exception.Message, Icon = SweetAlertIcon.Error });
    }
}