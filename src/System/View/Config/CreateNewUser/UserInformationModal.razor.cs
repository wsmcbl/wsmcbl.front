using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Config;

namespace wsmcbl.src.View.Config.CreateNewUser;

public partial class UserInformationModal : ComponentBase
{
    [Parameter] public UserEntity? UserInfo { get; set; }
}