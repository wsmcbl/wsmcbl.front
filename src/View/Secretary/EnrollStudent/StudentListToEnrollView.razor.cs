using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class StudentListToEnrollView : BaseView
{
    [Inject] protected EnrollStudentController? Controller { get; set; }
    private List<StudentDto>? studentList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadStudents();
    }

    private async Task LoadStudents()
    {
        studentList = await Controller!.GetStudentList();
    }

    protected override bool IsLoading()
    {
        return studentList == null;
    }
}