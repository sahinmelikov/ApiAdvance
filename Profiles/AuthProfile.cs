using AutoMapper;
using WebApiAdvance.Entities.Auth;
using WebApiAdvance.Entities.Dtos.Auth;

namespace WebApiAdvance.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
                
        CreateMap<RegisterDto,AppUser>();  //////Yeni RegisterDto nu Gonderecem AppUsernen Yoxla
       CreateMap<LoginDto, AppUser>();
        }
    }
}
