using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class InitializeEnrollmentView
{
    [Parameter] public string? degreeId { get; set; }
    [Parameter] public DegreeEntity? DegreeObj { get; set; }

    [Parameter] public EventCallback<DegreeEntity?> DegreeObjChanged { get; set; }
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

    private Task SaveEnrollments()
    {
        throw new NotImplementedException();
    }
}
