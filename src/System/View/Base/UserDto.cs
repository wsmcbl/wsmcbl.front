namespace wsmcbl.src.View.Base;

public class UserDto
{
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public string email { get; set; }
    public bool isActive { get; set; }
    public string role { get; set; }

    public UserDto()
    {
        
    }
    
}