namespace wsmcbl.src.Model.Academy;

public class PartialEntity
{
    public int partialId { get; set; }
    public string label { get; set; } = "N/A";
    public bool isActive { get; set; }
    public string semester { get; set; } = "N/A";
    public string period { get; set; } = "N/A";
    public int position { get; set; }
}