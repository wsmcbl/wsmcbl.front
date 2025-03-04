namespace wsmcbl.src.View.Config.UserInformationView;

public class EditUserDto
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public List<int> permissionList { get; set; } = [];
    public string? nextCloudGroup { get; set; } = null!;

    public bool IsDataValid()
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(nextCloudGroup))
        {
            return false;
        }
        
        return true;
    }
}