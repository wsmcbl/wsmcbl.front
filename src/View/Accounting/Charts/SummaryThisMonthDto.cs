namespace wsmcbl.src.View.Accounting.Charts;

public class SummaryThisMonthDto
{
    public FinancialData total { get; set; } = new();
    public FinancialData pastMonths { get; set; } = new();
    public FinancialData currentMonth { get; set; } = new();
    public FinancialData futureMonths { get; set; } = new();
    
    public class FinancialData
    {
        public decimal amount { get; set; }
        public int studentQuantity { get; set; }
    }
}