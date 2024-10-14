using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StateLoggingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Додаємо сервіси для MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Використовуємо middleware для логування помилок
app.UseMiddleware<ErrorLoggingMiddleware>();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
