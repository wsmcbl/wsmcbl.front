namespace wsmcbl.src.Model.Config;

public class UserEntity
{
    public string? userId { get; set; }
    public int roleId { get; set; }
    public bool isActive { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surName { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string userName { get; set; } = null!;
    public string password { get; set; } = null!;
    public string email { get; set; } = null!;
    
    public string getFullName() => $"{name} {secondName} {surName} {secondSurname}";

    public string getAlias()
    {
        return $"{name} {surName}";
    }
}