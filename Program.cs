using wsmcbl.front.Controllers;
using CurrieTechnologies.Razor.SweetAlert2;
using wsmcbl.front.Accounting;
using wsmcbl.front.Controllers.AcademyController;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddTransient<SweetAlertService>();
builder.Services.AddTransient<AlertService>();
builder.Services.AddTransient<TariffCollectionController>();
builder.Services.AddTransient<AcademyController>();  

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
