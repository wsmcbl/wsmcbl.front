using Microsoft.JSInterop;

namespace wsmcbl.src.Utilities;

public class Navigator
{
    private IJSRuntime JsRuntime { get; set; } = null!;

    public Navigator(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public async Task ToPage(string uri)
    {
        await JsRuntime.InvokeVoidAsync("window.open",uri, "_blank");   
    }

    public async Task InvokeModal(string identifier, string modal)
    {
        await JsRuntime.InvokeVoidAsync(identifier, modal);
    }
}