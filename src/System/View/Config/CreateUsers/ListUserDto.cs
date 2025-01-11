namespace wsmcbl.src.View.Config.CreateUsers;

public class ListUserDto
{
    public string userId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public int roleId { get; set; }
    public string RoleName => Enum.GetName(typeof(Role), roleId) ?? "Unknown";
}