using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace wsmcbl.src.Pages;

public class Index_razor : ComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    
    private List<string> months = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio" };
    private List<int> sales = new List<int> { 500, 600, 700, 800, 900, 1500 };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Cargar los scripts necesarios para la página
            await JSRuntime.InvokeVoidAsync("loadChartScripts");

            // Dar un pequeño retraso para asegurar que los scripts se carguen correctamente
            await Task.Delay(150);  // Ajusta este valor si es necesario.

            // Llama a la función createDynamicChart solo después de que los scripts se hayan cargado
            await JSRuntime.InvokeVoidAsync("createDynamicChart", months, sales);
        }
    }

    
}