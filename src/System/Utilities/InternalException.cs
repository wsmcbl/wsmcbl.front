namespace wsmcbl.src.Utilities;

public class InternalException : ArgumentException
{
    public string Title { get; private set; }
    public string Content { get; private set; }

    public InternalException(string content) : this("Error interno.", content) 
    {
    }

    public InternalException(string title, string content) : base($"{title} : {content}")
    {
        Title = title;
        Content = content;
    }
}