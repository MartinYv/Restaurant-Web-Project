using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant2.Data;
using System.Text.Json.Serialization;
using Restaurant.Web.Infrastucture.Extentions;
using NuGet.Packaging.Signing;


using static Restaurant.Common.GeneralApplicationConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/User/Login";
});


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount =
                         builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    options.Password.RequireLowercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    options.Password.RequireUppercase =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength =
        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
}).AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<RestaurantDbContext>();

builder.Services.AddControllersWithViews();

//Through this extended method we add all of the services throug reflection, the code bellow it(all the services) its not needed.
builder.Services.AddApplicationServices(typeof(IOrderService));



builder.Services.AddSession(); // asdasd

builder.Services.AddDistributedMemoryCache(); // This is required to store session data in memory
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Ensure the session cookie is only accessed through HTTP
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the session cookie is only sent over HTTPS
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout duration
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // asdasd


app.UseAuthentication();
app.UseAuthorization();

//Seeding administrator through the method we wrote. The first admin should be seeded manualy.
app.SeedAdministrator(DevelopmentAdminEmail);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
