namespace wsmcbl.src.View.Config.UserList;

public class UserToListDto
{
    public int roleId { get; set; }
    public string userId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string RoleName => Enum.GetName(typeof(Role), roleId) ?? "Unknown";
}