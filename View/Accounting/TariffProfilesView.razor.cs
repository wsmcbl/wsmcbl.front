using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Accounting;

namespace wsmcbl.front.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected SweetAlertService Swal { get; set; } = null!;
    
    protected List<StudentEntity>? students;
    
    protected override async Task OnInitializedAsync()
    {
        students = await Controller.getStudentList();
        if (students is null)
        {
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Sin estudiantes",
                Text = "La transacción no se completó correctamente.",
                Icon = SweetAlertIcon.Error
            });
        }
    }
}