namespace wsmcbl.src.View.Config;

public class LoginDto
{
    public string? token { get; set; }
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;

    public void setDefault()
    {
        token = string.Empty;
    }
}