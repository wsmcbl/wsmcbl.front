namespace wsmcbl.src.View.Accounting.TariffCollection;

public class EditDiscountDto
{
    public string? studentId { get; set; } = null!;
    public int discountId { get; set; }
    public string? authorizationToken { get; set; }
}