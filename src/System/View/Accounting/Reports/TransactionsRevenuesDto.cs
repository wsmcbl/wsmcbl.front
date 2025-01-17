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

    public string getStartDate()
    {
        return getDateTimeFormat(startDate);
    }

    public string getEndDate()
    {
        return getDateTimeFormat(endDate);
    }

    private string getDateTimeFormat(string date)
    {
        var result = DateTime.TryParse(date, out var value);
        return result ? value.ToString("dddd dd/MMM/yyyy, h:mm tt", new System.Globalization.CultureInfo("es-ES"))
                : "Sin fecha.";
    }
}