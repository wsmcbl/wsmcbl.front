namespace wsmcbl.src.View.Config.Authentication;

public class LoginDto
{
    public string? token { get; set; }
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;

    public void SetAsDefault()
    {
        token = string.Empty;
        email = string.Empty;
        password = string.Empty;
    }

    public LoginDto()
    {
    }

    public LoginDto(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}