using Microsoft.AspNetCore.Mvc;
using SimpleWebSite.Service;

namespace SimpleWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWord _word;

        public HomeController(IWord word)
        {
            _word = word;
        }
        public IActionResult Index()
        {
            return View(nameof(Index), _word.GetWord());
        }
    }
}