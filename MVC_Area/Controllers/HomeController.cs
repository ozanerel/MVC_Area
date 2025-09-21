using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Area.Models;
using MVC_Area.Models.Entities;
using MVC_Area.Models.ViewModels.AppUserViewModels;
using MVC_Area.Services.Abstracts;

namespace MVC_Area.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;//Rol ekleme,silme, güncelleme işlemleri için kullanılır.
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager,IAppUserService appUserService)
        {
            _logger = logger;
            //_userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appUserService = appUserService;
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
                //ViewModel dönüşümüdür
                #region İhtiyacımız kalmadı appuserservice sayesinde
                //AppUser user = new AppUser
                //{
                //    UserName = registerViewModel.UserName,
                //    Email = registerViewModel.Email,
                //    Address = "Kadıköy"
                //}; 
                //var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                
                #endregion


                var result = await _appUserService.RegisterUser(registerViewModel);

                if (result.Succeeded)
                {
                    //Kullaıcıya rol ekleyen metot
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return View(registerViewModel);
                }

                //if (result.Succeeded)
                //{
                //    //Kadolan kullanıcının rolü "kullanıcı" olmalı 
                //    await _userManager.AddToRoleAsync(user, "kullanıcı");
                //    return RedirectToAction("Index");//Eğer modeldeki tüm propertyler dolu ise index sayfasına yönlendir.
                //}
                //else
                //{
                //    foreach (var error in result.Errors)
                //    {
                //        ModelState.AddModelError(error.Code,error.Description);
                //    }
                //    return View(registerViewModel);
                //}

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {

                #region Servis tarafında kullanılan alan(eski işlem)
                //var existsUser = await _userManager.FindByEmailAsync(loginView.Email);//Girilen emaili bizim veritabanımızda arar.

                //if (existsUser != null)
                //{
                //    var signIn = await _signInManager.PasswordSignInAsync(existsUser, loginView.Password, false, false);
                //    if (signIn.Succeeded)
                //    {
                //        TempData["Success"] = "Giriş Başarılı";
                //        return RedirectToAction("Index");
                //    }
                //    else
                //    {
                //        TempData["Error"] = "Şifreniz Hatalı!";
                //        return View(loginView);
                //    }
                //}
                //else
                //{
                //    TempData["Error"] = "Böyle bir kullanıcı bulunamadı";
                //    return View(loginView);
                //} 
                #endregion

                var result = await _appUserService.SignInAsync(loginView,loginView.Password);

                if (result.Succeeded)
                {
                    TempData["Success"] = "Giriş Başarılı";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Giriş Başarısız!";//Şifreyi kontrol için servis üzerinden ekstra kontrol etmemiz gerekli
                    return View(loginView);
                }
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(error.Code, error.Description);
                //}
                //return View(registerViewModel);
            }
            else
            {
                return View(loginView);
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
