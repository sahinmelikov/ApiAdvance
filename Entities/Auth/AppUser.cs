using Microsoft.AspNetCore.Identity;

namespace WebApiAdvance.Entities.Auth
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        
    }
}
