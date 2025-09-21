using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Area.Areas.InsanKaynaklari.Controllers
{
    [Area("InsanKaynaklari")]
    //İnsan kaynaklarındaki scaffoldingreadme içerisindeki endpoint kodunu bu sefer ellemedik çünkü muhasebe gibi kullancağız yani url ye bu sefer muhasebe yerine insan kaynakları yazdığımızda zaten o sayfa gelecek
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
