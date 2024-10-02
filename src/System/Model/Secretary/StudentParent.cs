namespace wsmcbl.src.Model.Secretary;

public class StudentParent
{
    public string? parentId { get; set; }
    public bool sex { get; set; }
    public string name { get; set; }
    public string idCard { get; set; }
    public string occupation { get; set; }

    public bool isValid()
    {
        return name != "Sin asignar" || occupation != "Sin asignar";
    }

    public void init()
    {
        parentId = string.Empty;
        sex = true;
        name = "Sin asignar";
        idCard = "Sin asignar";
        occupation = "Sin asignar";
    }
}