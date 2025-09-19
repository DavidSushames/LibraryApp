using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FullMetalLibrary.Data;
using FullMetalLibrary.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FullMetalLibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FullMetalLibraryContext") ?? throw new InvalidOperationException("Connection string 'FullMetalLibraryContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Enable session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);// this will auto logout after 30 mins 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

   SeedData.Initialize(services);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admins}/{action=Login}/{id?}");

app.Run();
