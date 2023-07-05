using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class CustomProfileService : IProfileService
    {

        readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email , user.Email),

            new Claim(JwtRegisteredClaimNames.GivenName , user.Name),

            new Claim("Surname" , user.Surname),

        };

            context.AddRequestedClaims(claims); //Userinfo Token
            context.IssuedClaims.AddRange(claims); //JWT
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            context.IsActive = user != null;
        }
    }
}
