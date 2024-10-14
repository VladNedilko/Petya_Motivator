using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StateLoggingApp.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Логування помилки у файл
                var logFilePath = "error_log.txt";
                await File.AppendAllTextAsync(logFilePath, $"[{DateTime.Now}] - Error: {ex.Message}{Environment.NewLine}");
                throw; // Продовжити обробку помилки
            }
        }
    }
}
