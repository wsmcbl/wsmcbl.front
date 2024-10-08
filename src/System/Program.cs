using wsmcbl.src.Controller;
using CurrieTechnologies.Razor.SweetAlert2;
using wsmcbl.src.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddTransient<SweetAlertService>();
builder.Services.AddTransient<Notificator>();
builder.Services.AddTransient<ApiConsumer>();

builder.Services.AddTransient<Navigator>();

builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<IEnrollStudentController, EnrollStudentController>();
builder.Services.AddTransient<CreateOfficialEnrollmentController>();
builder.Services.AddTransient<PrintReportCardStudentController>();
builder.Services.AddTransient<CreateStudentProfileController>();

builder.Services.AddSweetAlert2();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();