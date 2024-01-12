using Business.Services;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MVC.Settings;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") 
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

#region Authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Login";
        config.AccessDeniedPath = "/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(AppSettings.CookieExpirationInMinutes);
        config.SlidingExpiration = true;
    });
#endregion

#region AppSettings
var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());
#endregion

// Add services to the container.
#region IoC (Inversion of Control) Container
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});
#endregion

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

#region Authentication
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllerRoute(name: "login",
    pattern: "login",
    defaults: new { controller = "Users", action = "Login" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
