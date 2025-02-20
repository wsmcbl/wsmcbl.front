using wsmcbl.src.Controller;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddSweetAlert2();
builder.Services.AddTransient<SweetAlertService>();
builder.Services.AddTransient<Navigator>();
builder.Services.AddTransient<Notificator>();
builder.Services.AddTransient<JwtClaimsService>();
builder.Services.AddTransient<ApiConsumerFactory>();
builder.Services.AddTransient<CustomAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());

// I iteration
builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<CreateSchoolYearController>();
builder.Services.AddTransient<UpdateOfficialEnrollmentController>();
builder.Services.AddTransient<EnrollStudentController>();
builder.Services.AddTransient<PrintReportCardStudentController>();

// II iteration
builder.Services.AddTransient<CreateStudentProfileController>();
builder.Services.AddTransient<AddingStudentGradesController>();
builder.Services.AddTransient<LoginController>();

// III iteration
builder.Services.AddTransient<TransactionReportByDateController>();
builder.Services.AddTransient<UpdateStudentController>();

// IV iteration
builder.Services.AddTransient<CreateUserController>();
builder.Services.AddTransient<CreateEnrollmentController>();
builder.Services.AddTransient<ChangeEducationLevelController>();
builder.Services.AddTransient<PrintDocumentController>(); //(Auxiliar)
builder.Services.AddTransient<DebtopController>(); 
builder.Services.AddTransient<EnablePartialGradeRecordingController>(); 


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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();