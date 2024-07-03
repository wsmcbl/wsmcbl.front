namespace wsmcbl.front.Models.Accounting.Output;
public class TransactionDto
{
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public DateTime dateTime { get; set; }

    public List<DetailDto> details { get; set; } = null!;
}