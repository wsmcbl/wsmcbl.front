using wsmcbl.front.View.Accounting.TariffCollection;

namespace wsmcbl.front.Model.Accounting;

public class TransactionEntity
{
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public DateTime dateTime { get; set; }

    public List<DetailDto> details { get; set; } = null!;
}