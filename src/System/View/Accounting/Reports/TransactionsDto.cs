namespace wsmcbl.src.View.Accounting.Reports;

public class TransactionsDto
{
    public int number { get; set; }
    public string studentName { get; set; } = null!;
    public string datetime { get; set; } = null!;
    public float amount { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }
}