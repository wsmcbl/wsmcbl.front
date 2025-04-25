using System.Text.Json;

namespace wsmcbl.src.Controller.Service;

public class TurnstileService
{
    private readonly IHttpClientFactory _clientFactory;

    public TurnstileService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    
    public class TurnstileResponse
    {
        public bool Success { get; set; }
        public List<string>? ErrorCodes { get; set; }
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var client = _clientFactory.CreateClient();
        var secret = Environment.GetEnvironmentVariable("CAPTCHA");
        Console.Write(secret);
        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new InvalidOperationException("La variable de entorno 'TURNSTILE' no está configurada.");
        }
        
        var response = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", 
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"secret", secret!},
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