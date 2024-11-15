namespace wsmcbl.src.Utilities;
using System.Security.Claims;
using System.Text.Json;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        if (string.IsNullOrEmpty(jwt))
        {
            throw new ArgumentException("El token JWT es nulo o vacío.");
        }

        var parts = jwt.Split('.');
        if (parts.Length != 3)
        {
            throw new FormatException("El token JWT no tiene el formato correcto.");
        }
        
        var payload = parts[1];
        var jsonBytes = Convert.FromBase64String(PadBase64(payload));
        
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        if (keyValuePairs == null)
        {
            throw new InvalidOperationException("No se pudieron obtener los claims del token.");
        }

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty));
    }

    private static string PadBase64(string base64)
    {
        return (base64.Length % 4) switch
        {
            2 => base64 + "==",
            3 => base64 + "=",
            _ => base64
        };
    }
}
