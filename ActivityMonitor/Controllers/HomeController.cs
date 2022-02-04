using Microsoft.AspNetCore.Mvc;

namespace ActivityMonitor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
