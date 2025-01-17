using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class InitializeEnrollmentView
{
    [Parameter] public DegreeEntity? DegreeObj { get; set; }
    [Parameter] public EventCallback<DegreeEntity?> DegreeObjChanged { get; set; }
    [Inject] public CreateEnrollmentController Controller { get; set; } = null!;
    [Inject] public Notificator Notificator { get; set; } = null!;
    protected SaveInitializerDto Initializer { get; set; } = new();
    public int Counter2 { get; set; }
    public int Counter { get; set; }

    
    private async Task UpdateDegreeObj(DegreeEntity? newDegree)
    {
        DegreeObj = newDegree;
        await DegreeObjChanged.InvokeAsync(DegreeObj);
    }
    
    private bool IsLoading()
    {
        return DegreeObj is not null;
    }

    private async Task SaveEnrollments(string enrollmentId)
    {
        var enroll = DegreeObj!.EnrollmentList!.FirstOrDefault(t => t.enrollmentId == enrollmentId);
        
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

        await Notificator.ShowError("Hubo un fallo al completar la tarea");
    }
}
