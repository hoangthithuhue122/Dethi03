using HoangThiThuHue_241230737.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews(o =>
{
    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
       _ => "Nguyen gia phai la so tu 1 den 2000");
    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (v, f) => "Nguyen gia phai la so tu 1 den 2000");
});

builder.Services.AddDbContext<QLTaiSanContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QLTaiSan")));
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
