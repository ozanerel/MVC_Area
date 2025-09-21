using Microsoft.AspNetCore.Identity;

namespace MVC_Area.Models.Context.Seeds
{
    public class AppUserRoleSeed
    {
        public static List<IdentityRole> Roles = new List<IdentityRole>
        {
            new IdentityRole{Id = Guid.NewGuid().ToString(),Name = "admin"},
            new IdentityRole{Id = Guid.NewGuid().ToString(),Name = "muhasebe"},
            new IdentityRole{Id = Guid.NewGuid().ToString(),Name = "insan kaynakları"},
            new IdentityRole{Id = Guid.NewGuid().ToString(),Name = "kullanıcı "},
        };
    }
}
