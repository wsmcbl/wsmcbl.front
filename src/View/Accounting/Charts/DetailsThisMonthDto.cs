namespace wsmcbl.src.View.Accounting.Charts;

public class DetailsThisMonthDto
{
    public TotalDto total { get; set; } = new();
    public LevelDto preschool { get; set; } = new();
    public LevelDto elementary { get; set; } = new();
    public LevelDto secondary { get; set; } = new();


    public class TotalDto
    {
        public decimal amount { get; set; }
        public int studentQuantity { get; set; }
    }

    public class LevelDto
    {
        public decimal tariffAmount { get; set; }
        public TotalDto total { get; set; } = new();
        public StudentGroupDto regularStudent { get; set; } = new();
        public StudentGroupDto discountedStudent { get; set; } = new();
    }

    public class StudentGroupDto
    {
        public decimal amount { get; set; }
        public int studentQuantity { get; set; }
    }
}