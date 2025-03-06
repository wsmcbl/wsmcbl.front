using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace wsmcbl.src.Utilities;

public class Navigator
{
    private readonly IJSRuntime jsRuntime;
    private readonly NavigationManager navigation;

    public Navigator(IJSRuntime jsRuntime, NavigationManager navigation)
    {
        this.jsRuntime = jsRuntime;
        this.navigation = navigation;
    }

    public void ToPage(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new InternalException("Uri must be not null.");
        }
        
        navigation.NavigateTo(uri, true);
    }
    
    public void UpdateUrl(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            throw new InternalException("Uri must be not null.");
        }
        
        navigation.NavigateTo(uri, forceLoad: false);
    }

    public string GetUrl()
    {
        return navigation.Uri;
    }

    public async Task ShowModal(string modalId)
    {
        await jsRuntime.InvokeVoidAsync("eval", $"$('#{modalId}').modal('show');");
    }

    public async Task HideModal(string modalId)
    {
        await jsRuntime.InvokeVoidAsync("eval", $"$('#{modalId}').modal('hide');");
    }

    public async Task ShowPdfModal()
    {
        await ShowModal("PdfViewerModal");
    }
}