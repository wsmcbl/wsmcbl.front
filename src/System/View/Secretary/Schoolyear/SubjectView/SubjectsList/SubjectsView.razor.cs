using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.SubjectsList;

public partial class SubjectsView : BaseView
{
    [Inject] private CreateSubjectDataController Controller { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private List<SubjectDataEntity>? Subjects { get; set; }
    private List<DegreeDataEntity> DegreeList { get; set; } = new();
    private List<SubjectAreaEntity> AreaList { get; set; } = new();
    private SubjectDataEntity EditSubject { get; set; } = new();
    private int SelectedDegreeId { get; set; }
        
    private void FilterByDegree(int degreeId)
    {
        SelectedDegreeId = degreeId;
    }
    protected override async Task OnParametersSetAsync()
    {
        Subjects = await Controller.GetSubjectList();
        DegreeList = await Controller.GetDegreeDataList();
        AreaList = await Controller.GetAreaList();
    }
    protected override bool IsLoading()
    {
        return Subjects == null;
    }

    private async Task CreateNewSubject()
    {
        await Navigator.ShowModal("ModalNewSubject");
    }

    private async Task OpenEditModal(SubjectDataEntity subject)
    {
        EditSubject = subject;
        await Navigator.ShowModal("ModalEditSubject");
    }

    //Move positions method
    private async Task UpdatePosition(bool moveUp, SubjectDataEntity subject)
    {
        var group = GetSubjectGroup(subject).ToList();
        var currentIndex = group.FindIndex(s => s.subjectDataId == subject.subjectDataId);
        
        if ((moveUp && currentIndex == 0) || (!moveUp && currentIndex == group.Count - 1))
        {
            return;
        }
    
        var targetIndex = moveUp ? currentIndex - 1 : currentIndex + 1;
        var targetSubject = group[targetIndex];
    
        (subject.number, targetSubject.number) = (targetSubject.number, subject.number);
    
        try 
        {
            await Controller.UpdateSubjectData(targetSubject);
            await Controller.UpdateSubjectData(subject);
            await OnParametersSetAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating positions: {ex.Message}");
            (subject.number, targetSubject.number) = (targetSubject.number, subject.number);
        }
    }
    private bool IsFirstInGroup(SubjectDataEntity subject)
    {
        var group = GetSubjectGroup(subject);
        return group.First().subjectDataId == subject.subjectDataId;
    }

    private bool IsLastInGroup(SubjectDataEntity subject)
    {
        var group = GetSubjectGroup(subject);
        return group.Last().subjectDataId == subject.subjectDataId;
    }

    private IEnumerable<SubjectDataEntity> GetSubjectGroup(SubjectDataEntity subject)
    {
        return Subjects!
            .Where(s => s.degreeDataId == subject.degreeDataId && s.areaId == subject.areaId)
            .OrderBy(s => s.number);
    }
}