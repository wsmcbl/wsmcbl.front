using Microsoft.AspNetCore.Components;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.SubjectsList;

public partial class SubjectsView : BaseView
{
    
    private List<SubjectsBasicDto>? Subjects { get; set; }
    protected override bool IsLoading()
    {
        return Subjects == null;
    }
}