namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DebDto(string studentId, int tariffId, string authToken)
{
    public string studentId { get; set; } = studentId;
    public int tariffId { get; set; } = tariffId;
    public string authorizationToken { get; set; } = authToken;
}