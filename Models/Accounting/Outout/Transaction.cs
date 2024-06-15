namespace wsmcbl.front.Models.Accounting;

using Newtonsoft.Json;

public class TransactionDto
{
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public DateTime dateTime { get; set; }

    public List<TariffPost> details { get; set; } = null!;
    
}