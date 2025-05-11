using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View.Base;

public abstract class BaseView : ComponentBase
{
    protected virtual bool IsLoading()
    {
        return false;
    }
}
