using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAdvance.Entities.Auth;
using WebApiAdvance.Entities.Dtos.Auth;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _appUser;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly TokenOption _tokenOption;

        public AuthController(UserManager<AppUser> appUser, RoleManager<IdentityRole> roleManager, IMapper mapper = null, IConfiguration configuration = null, TokenOption tokenOption = null)
        {
            _appUser = appUser;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _tokenOption = tokenOption;
        }
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser appUser=_mapper.Map<AppUser>(registerDto);
            var CreateResault = await _appUser.CreateAsync(appUser, registerDto.Password);
            if (!CreateResault.Succeeded) ////////// yeniki create emeliyyati ugurlu Olmayibsa meselen max leng 8 yazmisiq 6 gelibse
            {

                return BadRequest(new
                {
                    StatusCode = 400,
                    errors= CreateResault.Errors

                }) ;

            }
            await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            var addRoleResault = await _appUser.AddToRoleAsync(appUser, "admin");
            if (!addRoleResault.Succeeded) ////////// yeniki create emeliyyati ugurlu Olmayibsa meselen max leng 8 yazmisiq 6 gelibse
            {

                return BadRequest(new
                {
                    StatusCode = 400,
                    errors = addRoleResault.Errors

                });

            }
            return Ok(new
            {
                Message = "User Ugurla Qeydiyyatdan gecdi"
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            AppUser appUser = await _appUser.FindByNameAsync(loginDto.UserName);
            if (appUser is null) return NotFound();
            if (!await _appUser.CheckPasswordAsync(appUser, loginDto.Password))
            {
                return Unauthorized();
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id),
            new Claim(ClaimTypes.Name,appUser.UserName),
            new Claim("FullName",appUser.FullName)
        };

            IList<string> roles = await _appUser.GetRolesAsync(appUser);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtPayload payload = new JwtPayload(
                issuer: _tokenOption.Issuer,
                audience: _tokenOption.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration)
                );

            JwtSecurityToken securityToken = new JwtSecurityToken(header, payload);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(securityToken);
            return Ok(new
            {
                token = token,
                expires = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration)
            });
        }
    }
}
