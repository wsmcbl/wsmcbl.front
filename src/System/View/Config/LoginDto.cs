namespace wsmcbl.src.View.Config;

public class LoginDto
{
    public string token { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public void setDefault()
    {
        token = string.Empty;
    }
}