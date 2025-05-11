namespace wsmcbl.src.Model.Secretary;

public class SchoolyearEntity
{
    public string? id { get; set; }
    public string label { get; set; } = null!;
    public string? startDate { get; set; }
    public string? deadLine { get; set; }
    public bool isActive { get; set; }

    public void InitTariffAuxList()
    {
    }

    public void UpdateTariffList()
    {
    }
}