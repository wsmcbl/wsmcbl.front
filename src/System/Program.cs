using wsmcbl.src.Controller;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

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
builder.Services.AddTransient<CancelTransactionController>();
builder.Services.AddTransient<UpdateStudentController>();
builder.Services.AddTransient<UpdateStudentAccountingController>();
builder.Services.AddTransient<MoveStudentFromEnrollmentController>();


builder.Services.AddTransient<CreateUserController>();
builder.Services.AddTransient<CreateEnrollmentController>();
builder.Services.AddTransient<ChangeEducationLevelController>();
builder.Services.AddTransient<PrintDocumentController>(); //(Auxiliar)
builder.Services.AddTransient<ForgetDebtController>();
builder.Services.AddTransient<GenerateDebtorReportController>(); 
builder.Services.AddTransient<EnablePartialGradeRecordingController>(); 


builder.Services.AddTransient<ApplyArrearsController>(); 
builder.Services.AddTransient<CreateBackupController>(); 
builder.Services.AddTransient<GenerateStudentRegisterController>(); 
builder.Services.AddTransient<UpdateRolesController>(); 
builder.Services.AddTransient<ViewEnrollmentGuideController>(); 
builder.Services.AddTransient<CreateSchoolyearController>();
builder.Services.AddTransient<CreateSubjectDataController>();
builder.Services.AddTransient<CreateTariffDataController>();
builder.Services.AddTransient<ViewPrincipalDashboardController>();

builder.Services.AddTransient<GeneratePerformanceReportBySection>();
builder.Services.AddTransient<GenerateEvaluationStatsBySectionController>();
builder.Services.AddTransient<UnenrollController>();
builder.Services.AddTransient<GetSchoolYearServices>();
builder.Services.AddTransient<DashboardCashierController>();

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