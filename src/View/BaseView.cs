using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View;

public class BaseView : ComponentBase
{
    protected virtual bool IsLoad()
    {
        return false;
    }
}