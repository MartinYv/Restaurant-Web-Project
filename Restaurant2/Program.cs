
using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Models;
using Restaurant.Services.Data;
using Restaurant.Services.Data.Interfaces;
using Restaurant2.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<RestaurantDbContext>();

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDishTypeService, DishTypeService>();
builder.Services.AddScoped<IDishService, DishService>();

builder.Services.AddScoped<IMenuTypeService, MenuTypeService>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddScoped<ITableService, TableService>();

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
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // asdasd


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
