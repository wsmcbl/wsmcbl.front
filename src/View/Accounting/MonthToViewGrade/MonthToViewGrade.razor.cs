using System.Globalization;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.MonthToViewGrade;

public partial class MonthToViewGrade : BaseView
{
    [Inject] protected PrintDocumentController Controller { get; set; } = null!;
    [Inject] protected Notificator notificator { get; set; } = null!;
    private List<MediaDto> Listmedia { get; set; } = new();
    private MediaDto MediaEdit = new();
    private bool isLoading = true;

    protected override async Task OnParametersSetAsync()
    {
        await LoadMedia();
    }

    private async Task LoadMedia()
    {
        try
        {
            isLoading = true;
            var result = await Controller.GetAllMedia();
            Listmedia = result ?? new List<MediaDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar medios: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private void UpdateMedia(int mediaId)
    {
        var item = Listmedia.Find(x => x.mediaId == mediaId);
        if (item != null)
        {
            MediaEdit = new MediaDto
            {
                mediaId = item.mediaId,
                type = item.type,
                schoolyearId = item.schoolyearId,
                value = item.value
            };
        }
    }

    private async Task SaveChange()
    {
        var mediaint = int.Parse(MediaEdit.value);
        var response = await Controller.UpdateMedia(mediaint);
        if (response)
        {
            await notificator.ShowSuccess($"Hemos actualizado correctamente el mes correspondiente para ver las calificaciones a {mediaint}");
            await LoadMedia();
        }
    }
    
    private string ObtenerNombreMes(string valor)
    {
        if (int.TryParse(valor, out int numeroMes) && numeroMes >= 1 && numeroMes <= 12)
        {
            string nombreMes = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat.GetMonthName(numeroMes);
            return char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
        }
    
        return "Mes no válido";
    }
}