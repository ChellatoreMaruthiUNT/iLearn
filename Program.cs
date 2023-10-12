using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iLearn.Data;
using iLearn.Areas.Identity.Data;
using iLearn.Services.Interfaces;
using iLearn.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("iLearnContextConnection") ?? throw new InvalidOperationException("Connection string 'iLearnContextConnection' not found.");

builder.Services.AddDbContext<iLearnContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<iLearnContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
