namespace wsmcbl.src.Utilities;

public class PagedRequest
{
    public string? SearchText { get; set; }
    public string? sortBy { get; set; }
    public bool isAscending { get; set; }
    public int CurrentPage { get; set; }
    public int pageSize { get; set; }
    public int Quantity { get; set; }

    public PagedRequest()
    {
        CurrentPage = 1;
        pageSize = 10;
        isAscending = true;
    }

    public override string ToString()
    {
        var queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(SearchText))
            queryParams.Add($"search={Uri.EscapeDataString(SearchText)}");

        if (!string.IsNullOrWhiteSpace(sortBy))
            queryParams.Add($"sortBy={Uri.EscapeDataString(sortBy)}");

        queryParams.Add($"isAscending={isAscending.ToString().ToLower()}");

        if (CurrentPage > 0)
            queryParams.Add($"page={CurrentPage}");

        if (pageSize > 0)
            queryParams.Add($"pageSize={pageSize}");

        if (Quantity > 0)
            queryParams.Add($"quantity={Quantity}");
        
        return queryParams.Count > 0 ? $"?{string.Join("&", queryParams)}" : string.Empty;
    }
}

