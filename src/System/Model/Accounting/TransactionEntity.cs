using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Model.Accounting;

public class TransactionEntity
{
    public string? transactionId { get; set; }
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public DateTime dateTime { get; set; }
    public List<DetailDto> details { get; set; } = null!;
}