namespace wsmcbl.src.Utilities;

public class InternalException : ArgumentException
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Details { get; private set; }
    
    public int StatusCode { get; }

    public InternalException(string content) : this("Error interno.", content) 
    {
    }

    public InternalException(string title, string content, string? details = null, int code = 400) : base($"{title} : {content}")
    {
        Title = title;
        Content = content;
        Details = details ?? string.Empty;
        StatusCode = code;
    }
}