using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.dto.Output;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public class ConfigSchoolYear : ComponentBase
{
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; }
    protected SchoolYearEntity SchoolYearEntity;
    
    protected GradeDto SelectedGrade;
    protected SubjectDto SubjectNew = new();
    protected SchoolYearTariffs SelectedTariff = new();
    protected List<DropdownList> DropdownDegreeLists = new();
    protected List<DropdownList> DropdownTypeTariffsLists = new();
    protected List<DropdownList> DropdownSemesterLists =
    [
        new DropdownList { Id = 1, Nombre = "Primer Semestre" },
        new DropdownList { Id = 2, Nombre = "Segundo Semestre" },
        new DropdownList { Id = 3, Nombre = "Ambos" }
    ];    
    protected List<DropdownList> DropdownModalityLists =
    [
        new DropdownList { Id = 1, Nombre = "Preescolar" },
        new DropdownList { Id = 2, Nombre = "Primaria" },
        new DropdownList { Id = 3, Nombre = "Secundaria" }
    ];
    
    
    protected override async Task OnParametersSetAsync()
    {
        int degreeId = 1;
        SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
        
        SchoolYearEntity Default = new SchoolYearEntity();
        SchoolYearEntity = await Controller.GetNewSchoolYears(Default);
        if (SchoolYearEntity == Default)
        {
            throw new InternalException("Es posible que exista mas de 2 años lectivos activos al mismo tiempo.");
        }
        
        foreach (var item in SchoolYearEntity.Degrees)
        {
            DropdownDegreeLists.Add(new DropdownList
            {
                Id = degreeId,
                Nombre = item.Label
            });
            degreeId++;
        }

        DropdownTypeTariffsLists = await Controller.GetTypeTariffList();
    }

    protected async Task SaveSchoolYear(SchoolYearEntity schoolYearEntity)
    {
            var response = await Controller.SaveNewSchoolYear(schoolYearEntity);
            if (response)
            {
                await Notificator.ShowSuccess("Éxito", "");
            }
            else
            {
                await Notificator.ShowError("");
            }
    }
    protected void SelectGrade(GradeDto grade)
    {
        SelectedGrade = grade;
    }
    protected void RemoveSubject(SubjectDto subject)
    {
        if (SelectedGrade != null)
        {
            SelectedGrade.Subjects.Remove(subject);
        }
    }
    protected async Task SaveNewSubject(SubjectDto subject)
    {
        var response = await Controller.CreateNewSubject(subject);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "Asignatura creada correctamente, recarge la página");
        }
    }
    
    protected async Task SaveNewTariff(SchoolYearTariffs tariff)
    {
        var response = await Controller.CreateNewTariff(tariff);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "Tarifa guardada correctamente");
        }
        else
        {
            await Notificator.ShowError("Error", "No pudimos guardar la tarifa");
        }
    }
    protected async Task EditTariff(SchoolYearTariffs tariff)
    {
        SelectedTariff = tariff;
        SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
        await JsRuntime.InvokeVoidAsync("eval", "$('#ModalEditTariff').modal('show');");
    }
    protected void RemoveTariff(SchoolYearTariffs tariff)
    {
        SchoolYearEntity.Tariffs?.Remove(tariff);
    }
    
    protected string GetSelectedClass(GradeDto grade)
    {
        return grade == SelectedGrade ? "selected-grade" : string.Empty;
    }
    
}