using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Area.Areas.Muhasebe.Controllers
{
    [Area("Muhasebe")]//Nereye ait area olduğunu belirten attribute
    [Authorize]  
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
