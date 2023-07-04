using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplication4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContextPool<OsfmspoContext>(o => o.UseSqlServer("ServerServer=DELL\\SQLEXPRESS; Database=OSFMSPO; Trusted_Connection=true; TrustServerCertificate=true;"));
builder.Services.AddDbContext<OsfmspoContext>(options =>
options.UseSqlServer("ServerServer=DELL\\SQLEXPRESS; Database=OSFMSPO; Trusted_Connection=true; TrustServerCertificate=true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CustomerMasters}/{action=SignIn}/{id?}");

app.Run();
