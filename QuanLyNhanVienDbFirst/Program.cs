using Microsoft.EntityFrameworkCore;
using QuanLyNhanVienDbFirst.Models;
using QuanLyNhanVienDbFirst.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(cfg => { 
    cfg.Cookie.Name = "huySession113";             
    cfg.IdleTimeout = new TimeSpan(0, 60, 0);
});

builder.Services.AddHttpContextAccessor();

//test
var connectionStrings = builder.Configuration.GetConnectionString("EmployeeDatabase");
builder.Services.AddDbContext<quanlynhanvien2Context>(options => 
                        options.UseMySql(connectionStrings, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.18-mariadb")));
builder.Services.AddScoped<IEmployee, EmployeeService>();


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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
