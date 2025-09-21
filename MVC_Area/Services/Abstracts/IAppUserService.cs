using Microsoft.AspNetCore.Identity;
using MVC_Area.Models.Entities;
using MVC_Area.Models.ViewModels.AppUserViewModels;

namespace MVC_Area.Services.Abstracts
{
    public interface IAppUserService
    {
        Task<IdentityResult> RegisterUser(RegisterViewModel registerViewModel);//Kayıt işlemini bünyesine dahil edecek 
        Task<IdentityResult> AddToRole(AppUser user, string roleName);//Kullanıcıyı role ekleme işlemi
        Task<SignInResult> SignInAsync(LoginViewModel user, string password);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
    }
}
