namespace wsmcbl.src.View.Accounting.Reports;

public class TransactionsRevenuesDto
{
    public string userName { get; set; }
    public string consultedIn { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public int validQuantity { get; set; }
    public int validTotal { get; set; }
    public int invalidQuantity { get; set; }
    public int invalidTotal { get; set; }
    public List<TransactionsDto> transactionList { get; set; }

    public TransactionsRevenuesDto()
    {
        userName = string.Empty;
        consultedIn = string.Empty;
        startDate = string.Empty;
        endDate = string.Empty;
        validQuantity = 0;
        validTotal = 0;
        invalidQuantity = 0;
        invalidTotal = 0;
        transactionList = [];
    }
}