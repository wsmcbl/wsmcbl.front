using wsmcbl.src.Controller;
using CurrieTechnologies.Razor.SweetAlert2;
using wsmcbl.src.Service;
using wsmcbl.src.View.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddTransient<SweetAlertService>();
builder.Services.AddTransient<AlertService>();
builder.Services.AddScoped<ApiConsumer>();

builder.Services.AddTransient<CollectTariffController>();
builder.Services.AddTransient<IEnrollSudentController, EnrollStudentController>();  
builder.Services.AddTransient<CreateOfficialEnrollmentController>();

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