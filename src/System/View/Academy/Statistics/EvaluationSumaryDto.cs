namespace wsmcbl.src.View.Academy.Statistics;

public class EvaluationSumaryDto
{
    public GenderStats initialQuantity { get; set; } = new();
    public GenderStats currentQuantity { get; set; } = new();
    public GenderStats approved { get; set; } = new();
    public GenderStats failedFromOneToTwo { get; set; } = new();
    public GenderStats failedFromThreeToMore { get; set; } = new();
    public GenderStats notEvaluated { get; set; } = new();
}