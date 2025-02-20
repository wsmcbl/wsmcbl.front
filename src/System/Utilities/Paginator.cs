namespace wsmcbl.src.Utilities;

public class Paginator<T>
{
    public List<T> data { get; set; } = new();
    public int page { get; set; }
    public int pageSize { get; set; }
    public int quantity { get; set; }
    public int totalPages { get; set; }
}