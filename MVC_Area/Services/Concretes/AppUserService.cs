using Microsoft.AspNetCore.Identity;
using MVC_Area.Models.Entities;
using MVC_Area.Models.ViewModels.AppUserViewModels;
using MVC_Area.Services.Abstracts;

namespace MVC_Area.Services.Concretes
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRole(AppUser user, string roleName)
        {
            //Role ait başarılı ya da başarısız durumunu döndürüyoruz
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel registerViewModel)//Kullanıcı kayıt işlemini gerçekleştiriyor
        {
            //ViewModel dönüşümü
            AppUser user = new AppUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                Address = "Kadıköy"
            };


            //Kayıt işlemi
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);//Kullanıcıyı kaydediyoruz
            var roleResult = await AddToRole(user, "KULLANICI");//Kullanıcıyı role ekliyoruz
            if (roleResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }

        }

        public async Task<SignInResult> SignInAsync(LoginViewModel user, string password)
        {
            var existsUser = await _userManager.FindByEmailAsync(user.Email);

            if (existsUser != null)
            {
                var signIn = await _signInManager.PasswordSignInAsync(existsUser, user.Password, false, false);
                if (signIn.Succeeded)
                {
                    return SignInResult.Success;
                }
                else
                {
                    return SignInResult.Failed;
                }
            }
            else
            {
                return SignInResult.Failed;
            }
        }
    }
}
