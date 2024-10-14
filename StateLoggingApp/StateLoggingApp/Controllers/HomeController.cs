using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateLoggingApp.Models;
using System;

namespace StateLoggingApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FormModel());
        }

        [HttpPost]
        public IActionResult Index(FormModel model)
        {
            if (ModelState.IsValid)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = model.ExpiryDate,
                    HttpOnly = true
                };
                Response.Cookies.Append("StoredValue", model.Value, cookieOptions);

                return RedirectToAction("CheckCookies");
            }

            return View(model);
        }
        public IActionResult CheckCookies()
        {
            var cookieValue = Request.Cookies["StoredValue"];
            return View(new CheckCookiesModel { CookieValue = cookieValue });
        }
    }
}
