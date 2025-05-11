namespace wsmcbl.src.View.Accounting.Charts;

public class DistributionLevelsDto
{
    public TotalData total { get; set; } = new();
    public LevelData preschool { get; set; } = new();
    public LevelData elementary { get; set; } = new();
    public LevelData secondary { get; set; } = new();

    public class TotalData
    {
        public decimal amount { get; set; }
        public int studentQuantity { get; set; }
    }

    public class LevelData
    {
        public decimal amount { get; set; }
        public int studentQuantity { get; set; }
    }
}