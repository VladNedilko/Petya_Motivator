using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateLoggingApp.Models;
using System;

namespace StateLoggingApp.Controllers
{
    public class HomeController : Controller
    {
        // Обработчик GET-запроса для страницы с формой
        [HttpGet]
        public IActionResult Index()
        {
            // Возвращаем представление с пустой моделью
            return View(new FormModel());
        }

        // Обработчик POST-запроса для отправки формы
        [HttpPost]
        public IActionResult Index(FormModel model)
        {
            if (ModelState.IsValid)
            {
                // Записываем значение из формы в Cookies
                var cookieOptions = new CookieOptions
                {
                    Expires = model.ExpiryDate,
                    HttpOnly = true
                };
                Response.Cookies.Append("StoredValue", model.Value, cookieOptions);

                // Перенаправляем на страницу проверки Cookies
                return RedirectToAction("CheckCookies");
            }

            // В случае ошибок возвращаем форму с данными, чтобы пользователь мог их исправить
            return View(model);
        }

        // Обработчик GET-запроса для страницы проверки Cookies
        public IActionResult CheckCookies()
        {
            // Читаем значение из Cookies
            var cookieValue = Request.Cookies["StoredValue"];
            return View(new CheckCookiesModel { CookieValue = cookieValue });
        }
    }
}
