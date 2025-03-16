using Microsoft.IdentityModel.Tokens;

namespace wsmcbl.src.Utilities;

public static class BuilderService
{
    public static void ConfigAuthentication(this IServiceCollection Services)
    {
        Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.Audience = "wsmcbl.api";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateLifetime = true,
                RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            };
        });
    }
}