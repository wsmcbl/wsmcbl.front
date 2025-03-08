namespace wsmcbl.src.View.Config.UpdateRole;

public class RoleListDto
{
    public int roleId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public List<int> permissionList { get; set; }
    
    public RoleListDto()
    {
        roleId = 0;
        name = "N/A";
        description = "N/A";
        permissionList = new List<int>();
    }
}