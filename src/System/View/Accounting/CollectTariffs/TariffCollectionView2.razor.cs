using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.CollectTariffs.Dto;

namespace wsmcbl.src.View.Accounting.CollectTariffs;

public partial class TariffCollectionView2 : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; private set; } = null!;
    
    protected List<TariffDto>? TariffList { get; set; }
    protected StudentEntity? StudentEntity { get; private set; }
    protected bool IsLoading() => StudentEntity == null || TariffList == null;
    private async Task LoadStudent()
    {
        StudentEntity = await Controller.GetStudent(StudentId!);
        Controller.SetStudent(StudentEntity);
    }
    
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudentId))
        {
            throw new InternalBufferOverflowException("Student ID is not valid");
        }
        await LoadStudent();
        TariffList = await Controller.GetTariffList("student", StudentId!);
        
    }
    
    
}