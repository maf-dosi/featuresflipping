using Microsoft.AspNetCore.Mvc;

namespace SimpleWebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}