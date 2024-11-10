namespace wsmcbl.src.Model.Secretary;

public class StudentParent
{
    public string? parentId { get; set; }
    public bool sex { get; set; }
    public string name { get; set; }
    public string idCard { get; set; }
    public string occupation { get; set; }
    
    public bool isTutorPartiallyFilled()
    {
        bool anyFieldFilled = !string.IsNullOrWhiteSpace(name) ||
                              !string.IsNullOrWhiteSpace(idCard) ||
                              !string.IsNullOrWhiteSpace(occupation);
    
        bool allFieldsFilled = !string.IsNullOrWhiteSpace(name) &&
                               !string.IsNullOrWhiteSpace(idCard) &&
                               !string.IsNullOrWhiteSpace(occupation);

        return anyFieldFilled && !allFieldsFilled;
    }

    public bool isTutorEmpty()
    {
        return string.IsNullOrWhiteSpace(name) &&
               string.IsNullOrWhiteSpace(idCard) &&
               string.IsNullOrWhiteSpace(occupation);
    }

    public void SetDefault()
    {
        parentId = string.Empty;
        sex = true;
        name = "Sin registrar";
        idCard = "Sin registrar";
        occupation = "Sin registrar";
    }
    
    public void init()
    {
        parentId = string.Empty;
        sex = true;
        name = "";
        idCard = "";
        occupation = "";
    }
}