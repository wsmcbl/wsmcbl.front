namespace wsmcbl.src.View.Accounting.Transactions;

public class TransactionFullDto
{
    public string transactionId { get; set; }
    public string studentId { get; set; }
    public string studentName { get; set; }
    public string enrollmentLabel { get; set; }
    public int total { get; set; }
    public string dateTime { get; set; }
    public int type { get; set; }
    public bool isValid { get; set; }

    public TransactionFullDto()
    {
        transactionId = "";
        studentId = "";
        studentName = "";
        enrollmentLabel = "";
        total = 0;
        dateTime = "";
        type = 0;
        isValid = false;
    }
}