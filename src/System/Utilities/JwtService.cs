namespace wsmcbl.src.Utilities;


public class JwtService
{
    private readonly HttpClient _httpClient;

    public JwtService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username, password });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
        return result?.Token;
    }
}

public class JwtResponse
{
    public string Token { get; set; }
}
