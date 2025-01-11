namespace wsmcbl.src.View.Components.CreateNewUser;

public class CreateUserDto
{
    public int roleId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public List<int> permissionList { get; set; } = new List<int>();
    public string nextCloudGroup { get; set; } = null!;

    public bool IsDataValid()
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(nextCloudGroup))
        {
            return false;
        }

        if (permissionList.Count <= 0 || roleId == 0)
        {
            return false;
        }
        
        return true;
    }
    
    
    
}