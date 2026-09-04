using DisasterAlleviationApp.Data; // Imports your Data namespace so Program.cs knows about ApplicationDbContext
using Microsoft.EntityFrameworkCore; // Imports EF Core database extensions

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adds MVC (Model-View-Controller) services so the app knows how to handle web pages and controllers
builder.Services.AddControllersWithViews();

// Registers your ApplicationDbContext with the Dependency Injection container
// It tells the app: "Use SQL Server, and fetch the connection string named 'DefaultConnection' from appsettings.json"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
