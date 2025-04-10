using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Components.UnenrollmentsView;

public partial class UnenrollmentsView : BaseView
{
    [Inject] UnenrollmentController Controller { get; set; } = null!;
    private List<UnenrollmentBasicDto>? StudentsList { get; set; }


    protected override async Task OnParametersSetAsync()
    {
        StudentsList = await Controller.GetStudents();
    }


    protected override bool IsLoading()
    {
        return StudentsList == null;
    }
}