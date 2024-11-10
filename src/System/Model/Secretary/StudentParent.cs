namespace wsmcbl.src.Model.Secretary;

public class StudentParent
{
    public string? parentId { get; set; }
    public bool sex { get; set; }
    public string name { get; set; }
    public string idCard { get; set; }
    public string occupation { get; set; }
    
    // Retorna true si al menos un campo está lleno, pero no todos
    public bool isTutorPartiallyFilled()
    {
        // Verifica si al menos uno de los campos está lleno
        bool anyFieldFilled = !string.IsNullOrWhiteSpace(name) ||
                              !string.IsNullOrWhiteSpace(idCard) ||
                              !string.IsNullOrWhiteSpace(occupation);
    
        // Verifica si todos los campos están llenos
        bool allFieldsFilled = !string.IsNullOrWhiteSpace(name) &&
                               !string.IsNullOrWhiteSpace(idCard) &&
                               !string.IsNullOrWhiteSpace(occupation);

        // Devuelve true solo si algunos campos están llenos, pero no todos
        return anyFieldFilled && !allFieldsFilled;
    }

    // Retorna true si todos los campos están vacíos
    public bool isTutorEmpty()
    {
        return string.IsNullOrWhiteSpace(name) &&
               string.IsNullOrWhiteSpace(idCard) &&
               string.IsNullOrWhiteSpace(occupation);
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