using Intex24_Group2_3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Intex24_Group2_3.Services;
using Intex24_Group2_3.Models;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = configuration["Authentication__Microsoft__ClientId"];
    microsoftOptions.ClientSecret = configuration["Authentication__Microsoft__ClientSecret"];
});

// Configure strongly typed settings objects
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

//// Add configuration services
//builder.Configuration.AddAzureKeyVault(
//    new Uri("https://intex24group23keyvault.vault.azure.net/"),
//    new DefaultAzureCredential());

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("IntexConnection") ?? throw new InvalidOperationException("Connection string 'IntexConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<ShoppingContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// When we refer to IWaterRepository, we actually want to use the EFWaterRepository
builder.Services.AddScoped<IShoppingRepository, EFShoppingRepository>();

builder.Services.AddRazorPages(); // Allows us to use MVVM

builder.Services.AddDistributedMemoryCache(); // Allows us to use session state
builder.Services.AddSession(); // Allows us to use session state

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Looks at the first route, and then the second, etc., but don't show the ones after a route is found
app.MapDefaultControllerRoute();
app.MapControllerRoute("pageenumandtype", "{projectType}/{pageNum}", new { Controller = "Home", action = "Shop" });
app.MapControllerRoute("pagination", "{pageNum}", new { Controller = "Home", action = "Shop", pageNum = 1 });
app.MapControllerRoute("projectType", "{projectType}", new { Controller = "Home", action = "Shop", pageNum = 1 });
// app.MapControllerRoute("default", "/Identity/Account/Login", new { Controller = "Home", action = "Shop" });

// Logging route information
app.Use(async (context, next) =>
{
    var routeData = context.GetRouteData();
    var action = routeData?.Values["action"];
    var controller = routeData?.Values["controller"];
    var path = context.Request.Path;
    Console.WriteLine($"Route: {controller}/{action} - Path: {path}");
    await next();
});

app.MapRazorPages();

app.Run();
