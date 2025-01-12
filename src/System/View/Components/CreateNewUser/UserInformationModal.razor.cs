using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Config;
using wsmcbl.src.View.Config.CreateUsers;

namespace wsmcbl.src.View.Components.CreateNewUser;

public partial class UserInformationModal : ComponentBase
{
    [Parameter] public UserEntity? UserInfo { get; set; }
}