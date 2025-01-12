namespace wsmcbl.src.Model.Config;

public class UserEntity
{
    public string userId { get; set; } = null!;
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surName { get; set; } = null!;
    public string? secondSurname { get; set; }
    public string userName { get; set; } = null!;
    public string password { get; set; } = null!;
    public string email { get; set; } = null!;
    public bool isActive { get; set; }
    public int roleId { get; set; }
    
    public string Fullname => $"{name} {secondName} {surName} {secondSurname}";
    
    
    
}