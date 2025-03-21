namespace wsmcbl.src.View.Accounting.Reports.Revenue;

public class TransactionDto
{
    public int number { get; set; }
    public string studentName { get; set; } = null!;
    public string datetime { get; set; } = null!;
    public decimal amount { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }
}