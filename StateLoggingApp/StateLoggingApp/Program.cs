using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StateLoggingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ������ ������ ��� MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ������������� middleware ��� ��������� �������
app.UseMiddleware<ErrorLoggingMiddleware>();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
