using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Area.Models.Entities;
using MVC_Area.Services.Abstracts;

namespace MVC_Area.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
      

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }
        public IActionResult Index()
        {
            
            return View(_userManager.Users.ToList());
        }
    }
}
