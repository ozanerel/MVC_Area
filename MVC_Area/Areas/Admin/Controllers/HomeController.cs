using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Area.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]//Oturumu hali hazırda açmış olacak açmazsa buraya ulaşamayacak
    //Parantez içerisinde rolünü verirsek bu sayede sadece giriş yapması yeterli olmayacak admin olması lazım
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
