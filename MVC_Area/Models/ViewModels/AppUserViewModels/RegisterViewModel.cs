using System.ComponentModel.DataAnnotations;

namespace MVC_Area.Models.ViewModels.AppUserViewModels
{
    public class RegisterViewModel
    {
        /*ViewModel'lar taşıyıcı nesnelerdir. Görevi sadece viewdan alınacak model nesnesini temsil edecek.Burada kullanıcıdan alınan verileri tutmak için kullanılır.Kullanıcıyla muhattap olacak kısımlar buralar direkt db içerikleri değil  */
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]//Boş geçilememesini sağlamak için bu attribute kullanılır.
        public string UserName { get; set; }//Boş geçilemez
        [Required(ErrorMessage = "Email boş geçilemez")]
        [EmailAddress(ErrorMessage = "Lütfen email formatınde bir değer giriniz")]//Email formatında olmasını sağlar.
        public string Email { get; set; }//Boş geçilemez
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }//Boş geçilemez
        [Required(ErrorMessage = "Şifre tekrar boş geçilemez")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]//Password ile ConfirmPassword'ün aynı olmasını sağlar.
        public string ConfirmPassword { get; set; }//Boş geçilemez
    }
}


