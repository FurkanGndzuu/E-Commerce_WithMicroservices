using IdentityService.API.Abstractions;
using IdentityService.API.DTOs;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUser(CreateUserDTO user)
        {
           
            var newUser = new ApplicationUser
            {
                Email = user.Email,
                UserName = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
               return false;
            }
            return true;
           
        }
    }
}
