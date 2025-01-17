using wsmcbl.src.Utilities;

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
    public List<TransactionDto> transactionList { get; set; }

    public TransactionsRevenuesDto()
    {
        userName = "...";
        consultedIn = "...";
        startDate = string.Empty;
        endDate = string.Empty;
        validQuantity = 0;
        validTotal = 0;
        invalidQuantity = 0;
        invalidTotal = 0;
        transactionList = [];
    }

    public string getValidTotalString()
    {
        return validTotal.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }

    public string getInvalidTotalString()
    {
        return invalidTotal.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }
}