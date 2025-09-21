using System.ComponentModel.DataAnnotations;

namespace MVC_Area.Models.ViewModels.AppUserViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email boş geçilemez")]
        [EmailAddress(ErrorMessage = "Lütfen email formatınde bir değer giriniz")]//Email formatında olmasını sağlar.
        public string Email { get; set; }//Boş geçilemez

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }//Boş geçilemez
    }
}
