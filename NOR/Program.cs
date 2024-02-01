using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NOR.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NORContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NORContext") ?? throw new InvalidOperationException("Connection string 'NORContext' not found.")));

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();


app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registos}/{action=Lista}/{id?}");

app.Run();
