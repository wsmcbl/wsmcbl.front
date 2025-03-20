namespace wsmcbl.src.View.Accounting.Reports.Revenue;

public class TransactionsRevenuesDto
{
    public SummaryDto summary { get; set; } = new SummaryDto();
    public List<TransactionDto> data { get; set; } = [];
    public int page { get; set; }
    public int pageSize { get; set; }
    public int quantity { get; set; }
    public int totalPages { get; set; }
}