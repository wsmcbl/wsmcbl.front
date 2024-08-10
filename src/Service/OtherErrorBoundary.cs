using Microsoft.AspNetCore.Components.Web;

namespace wsmcbl.src.Service;

public class OtherErrorBoundary : ErrorBoundary
{
    protected override Task OnErrorAsync(Exception exception)
    {
        return Task.CompletedTask;
    }
}