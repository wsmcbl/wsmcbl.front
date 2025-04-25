using System.Text.Json;

namespace wsmcbl.src.Controller.Service;

public class TurnstileService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _config;

    public TurnstileService(IHttpClientFactory clientFactory, IConfiguration config)
    {
        _clientFactory = clientFactory;
        _config = config;
    }
    
    public class TurnstileResponse
    {
        public bool Success { get; set; }
        public List<string>? ErrorCodes { get; set; }
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var client = _clientFactory.CreateClient();
        var secret = "0x4AAAAAABT_ZnFUiTDZQjO-LRTCHP1u5BM";

        var response = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", 
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"secret", secret},
                {"response", token}
            }));

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<TurnstileResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Success == true;
    }
}