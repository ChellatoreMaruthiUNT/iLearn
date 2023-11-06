using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iLearn.Data;
using iLearn.Areas.Identity.Data;
using iLearn.Services.Interfaces;
using iLearn.Services;
using Microsoft.AspNetCore.Http.Features;
using Azure.Identity;
using iLearn.iLearnDbModels;
using System.Configuration;
//using iLearn.iLearnDbModels;

var builder = WebApplication.CreateBuilder(args);

// uncomment when publishing
//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

var connectionString = builder.Configuration.GetConnectionString("iLearnContextConnection") ?? throw new InvalidOperationException("Connection string 'iLearnContextConnection' not found.");
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    connectionString = builder.Configuration.GetConnectionString("iLearnContextConnectionProduction") ?? throw new InvalidOperationException("Connection string 'iLearnContextConnection' not found.");
}


builder.Services.AddDbContext<iLearnContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<iLearnContext>();
builder.Services.AddDbContext<iLearnAppContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.Services.BuildServiceProvider().GetService<iLearnContext>().Database.Migrate();
    builder.Services.BuildServiceProvider().GetService<iLearnAppContext>().Database.Migrate();
}
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<ISectionsService, SectionsService>();
builder.Services.AddScoped<IEvaluationService, EvaluationService>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 52428800; // 50 MB limit
});
var app = builder.Build();


    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Index}/{id?}");

app.Run();
