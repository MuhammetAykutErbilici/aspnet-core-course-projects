using EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Models; // DataContext sınıfın hangi klasördeyse onu buraya eklemelisin

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DÜZELTİLEN BÖLÜM:
builder.Services.AddDbContext<DataContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database"); 
    options.UseSqlite(connectionString); // Buraya ; eklendi
}); // Parantez ve süslü parantez kapatıldı, ; eklendi

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();