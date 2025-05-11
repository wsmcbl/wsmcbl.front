using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace wsmcbl.src.Utilities;

public class CustomErrorBoundary : ErrorBoundary
{
    [Inject] protected Notificator? Notificator { get; set; }

    protected override async Task OnErrorAsync(Exception exception)
    {
        var title = "Surgió un error.";
        var error = $"{exception.Message}\n Trace: {exception.StackTrace}";
        
        if (exception is InternalException internalException)
        {
            title = internalException.Title;
            error = internalException.Content + internalException.StackTrace;
        }

        await Notificator!.ShowError(title, error);
    }
}