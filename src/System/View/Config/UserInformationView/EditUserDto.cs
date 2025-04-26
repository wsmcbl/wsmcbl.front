namespace wsmcbl.src.View.Config.UserInformationView;

public class EditUserDto
{
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public List<int> permissionList { get; set; } = [];
    public string? nextCloudGroup { get; set; } = null!;

    
    public class BasicUserInfoDto
    {
        public string name { get; set; } = null!;
        public string? secondName { get; set; }
        public string surname { get; set; } = null!;
        public string? secondSurname { get; set; }
        public string? nextCloudGroup { get; set; } = null!;
    }
    
    public BasicUserInfoDto ToBasicUserInfo()
    {
        return new BasicUserInfoDto
        {
            name = this.name,
            secondName = this.secondName,
            surname = this.surname,
            secondSurname = this.secondSurname,
            nextCloudGroup = this.nextCloudGroup
        };
    }
    
    public bool IsDataValid()
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(nextCloudGroup))
        {
            return false;
        }
        
        return true;
    }
    
}