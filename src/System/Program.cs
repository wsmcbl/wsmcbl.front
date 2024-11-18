using wsmcbl.src.Controller;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddTransient<SweetAlertService>();
builder.Services.AddTransient<Notificator>();
builder.Services.AddTransient<ApiConsumer>();
builder.Services.AddTransient<Navigator>();
builder.Services.AddSweetAlert2();

builder.Services.AddTransient<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ProtectedLocalStorage>();


builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<IEnrollStudentController, EnrollStudentController>();
builder.Services.AddTransient<CreateOfficialEnrollmentController>();
builder.Services.AddTransient<PrintReportCardStudentController>();
builder.Services.AddTransient<CreateStudentProfileController>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Audience = "wsmcbl.api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateLifetime = true,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    });


var app = builder.Build();


app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();