namespace wsmcbl.src.Service;

public class ErrorService
{
    public event Action<string> OnError;

    public void ShowError(string message)
    {
        OnError?.Invoke(message);
    }
}