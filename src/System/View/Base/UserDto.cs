namespace wsmcbl.src.View.Base;

public class UserDto
{
    public string name { get; set; } = null!;
    public string secondName { get; set; }
    public string surname { get; set; } = null!;
    public string secondSurname { get; set; }
    public string email { get; set; } = null!;
    public bool isActive { get; set; }
    public string role { get; set; } = null!;

    public UserDto()
    {
        name = "Default";
        surname = "User";
    }

    public string getName()
    {
        return $"{name} {surname}";
    }
}