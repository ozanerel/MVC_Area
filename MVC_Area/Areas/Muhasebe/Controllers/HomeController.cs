using Microsoft.AspNetCore.Mvc;

namespace MVC_Area.Areas.Muhasebe.Controllers
{
    [Area("Muhasebe")]//Nereye ait area olduğunu belirten attribute
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
