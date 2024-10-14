using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Anton.Controllers
{
    [Route("Library")]
    public class LibraryController : Controller
    {
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("Привітання в бібліотеці!");
        }

        [HttpGet("Books")]
        public IActionResult Books()
        {
            var books = _configuration.GetSection("Books").Get<string[]>();
            return Ok(books);
        }

        [HttpGet("Profile/{id?}")]
        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return Content("Інформація про користувача: Антон");
            }

            var users = _configuration.GetSection("Users").Get<string[]>();
            if (id >= 0 && id < users.Length)
            {
                return Content($"Інформація про користувача з ID {id}: {users[id.Value]}");
            }
            else
            {
                return Content("Користувача з таким ID не знайдено.");
            }
        }
    }
}
