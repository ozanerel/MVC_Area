using Microsoft.AspNetCore.Identity;

namespace MVC_Area.Models.Entities
{
    public class AppUser:IdentityUser//<int>
    //IdentityUser<int>  --> int tipinde bir primary key'e sahip user tablosu oluştur demektir.
    {
        //IdentityUser sınıfından alınan miras sayesinde bize sağlanmış olan user tablosuna ekleme yapabiliriz.
        public string? Address { get; set; }
    }
}
