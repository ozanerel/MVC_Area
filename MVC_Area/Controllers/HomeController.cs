using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Area.Models;
using MVC_Area.Models.Entities;
using MVC_Area.Models.ViewModels.AppUserViewModels;

namespace MVC_Area.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)//Modelin durumunu (dolu/boş) kontrol etmek için modelstate
            {
                //ViewModel dönüşümü
                AppUser user = new AppUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    Address = "Kadıköy"
                };


                //Kayıt işlemi
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                //Async = İş devam ederken başka işlemler de yapılabilir demek.
                //await = sıkıntı yaşatırsa bir sonraki istekte devam et demek.

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");//Eğer modeldeki tüm propertyler dolu ise index sayfasına yönlendir.
                }
                else
                {
                    
                    return View(registerViewModel);
                }


            }
            else
            {
                return View(registerViewModel);//Spanlara hata mesajlarını yazdırabilmek için modeli geri gönderiyoruz.
            }
           
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
