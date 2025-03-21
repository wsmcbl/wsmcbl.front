namespace wsmcbl.src.View.Accounting;

public class UpdateDiscountDto
{
    public string? studentId { get; set; } = null!;
    public int discountId { get; set; }
    public string? authorizationToken { get; set; }
}