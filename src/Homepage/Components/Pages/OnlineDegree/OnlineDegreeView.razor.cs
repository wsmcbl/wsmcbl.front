using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.Components.Pages.OnlineDegree;

public partial class OnlineDegreeView : ComponentBase
{
    [Inject] ViewGradeOnlineController Controller { get; set; } = null!;
    private ModelGrade Model = new();
    private byte[]? GradePdf { get; set; } = [];

    private async Task GetGrade()
    {
        try
        {
            GradePdf = await Controller.GetGradeByStudent(Model.StudentId, Model.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public class ModelGrade
    {
        [Required(ErrorMessage = "El código del estudiante es obligatorio")]
        public string? StudentId { get; set; }
        
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? Token { get; set; }
    }
    
}