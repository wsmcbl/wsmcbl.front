using Microsoft.AspNetCore.Components;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

public partial class CreateNewSchoolYearView : BaseView
{
    private CreateSchoolYearDto SchoolYear { get; set; } = new();
    private List<PartialsDto> PartialList = 
    [
        new() { semester = 1, partial = 1 },
        new() { semester = 1, partial = 2 },
        new() { semester = 2, partial = 1 },
        new() { semester = 2, partial = 2 }
    ];
    private void OnDateChanged(ChangeEventArgs e, int index, bool isStartDate)
    {
        if (!DateTime.TryParse(e.Value!.ToString(), out var selectedDate))
        {
            return;
        }

        if (isStartDate)
        {
            PartialList![index].startDate = new DateOnlyDto(selectedDate);
        }
        else
        {
            PartialList![index].deadLine = new DateOnlyDto(selectedDate);
        }
    }


    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private int currentStep = 1;
    private bool termsAccepted = false;
    private ArticleModel article = new ArticleModel();
    private void NextStep()
    {
        if (currentStep < 3) currentStep++;
    }
    private void PrevStep()
    {
        if (currentStep > 1) currentStep--;
    }
    private void FinishSetup()
    {
        Console.WriteLine("Configuración finalizada");
    }
    
    class ArticleModel
    {
        public string Name { get; set; } = "N/A";
        public string Description { get; set; } = "N/A";
        public string Category { get; set; } = "N/A";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Tax { get; set; } = 15; // 15% por defecto
    }
}