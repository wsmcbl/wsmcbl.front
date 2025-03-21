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

builder.Services.ConfigAuthentication();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());


builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<UpdateOfficialEnrollmentController>();
builder.Services.AddTransient<EnrollStudentController>();
builder.Services.AddTransient<PrintReportCardStudentController>();


builder.Services.AddTransient<CreateStudentProfileController>();
builder.Services.AddTransient<AddingStudentGradesController>();
builder.Services.AddTransient<LoginController>();


builder.Services.AddTransient<TransactionReportByDateController>();
builder.Services.AddTransient<UpdateStudentController>();


builder.Services.AddTransient<CreateUserController>();
builder.Services.AddTransient<CreateEnrollmentController>();
builder.Services.AddTransient<ChangeEducationLevelController>();
builder.Services.AddTransient<PrintDocumentController>(); //(Auxiliar)
builder.Services.AddTransient<ForgetDebtController>();
builder.Services.AddTransient<DebtopController>(); 
builder.Services.AddTransient<EnablePartialGradeRecordingController>(); 


builder.Services.AddTransient<ApplyArrearsController>(); 
builder.Services.AddTransient<BackupController>(); 
builder.Services.AddTransient<GenerateStudentRegisterController>(); 
builder.Services.AddTransient<UpdateRolesController>(); 
builder.Services.AddTransient<EnrollmentGuideController>(); 
builder.Services.AddTransient<GetDateMaxOfRecordingGradeController>();
builder.Services.AddTransient<CreateSchoolyearController>();
builder.Services.AddTransient<CreateSubjectDataController>();
builder.Services.AddTransient<CreateTariffDataController>();


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