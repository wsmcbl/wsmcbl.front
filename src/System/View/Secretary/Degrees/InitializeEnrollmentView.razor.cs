using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class InitializeEnrollmentView : BaseView
{
    [Parameter] public DegreeEntity? Degree { get; set; }
    [Parameter] public EventCallback<DegreeEntity?> DegreeChanged { get; set; }
    
    [Inject] public Notificator Notificator { get; set; } = null!;
    [Inject] public CreateEnrollmentController Controller { get; set; } = null!;
    
    private SaveInitializerDto Initializer { get; set; } = new();
    private int Counter { get; set; }
    private int Counter2 { get; set; }

    private async Task SaveEnrollments(string enrollmentId)
    {
        var enroll = Degree!.EnrollmentList!.FirstOrDefault(t => t.enrollmentId == enrollmentId);
        if (enroll is not null)
        {
            Initializer.enrollmentId = enroll.enrollmentId;
            Initializer.capacity = enroll.capacity;
            Initializer.section = enroll.section;
            Initializer.label = enroll.label;
        }

        var response = await Controller.InitializerEnrollment(Initializer);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado las secciones correctamente.");
            return;
        }

        await Notificator.ShowError("Hubo un fallo al completar la tarea.");
    }
    
    private async Task UpdateDegree(DegreeEntity? newDegree)
    {
        Degree = newDegree;
        await DegreeChanged.InvokeAsync(Degree);
    }
    
    protected override bool IsLoading()
    {
        return Degree == null;
    }
}
