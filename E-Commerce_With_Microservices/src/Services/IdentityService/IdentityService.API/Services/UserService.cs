using IdentityService.API.Abstractions;
using IdentityService.API.DTOs;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedService.Responses;
using System;
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

        public async Task<Response<NoContent>> CreateUser(CreateUserDTO user)
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
                return Response<NoContent>.Fail("User is not created", StatusCodes.Status400BadRequest);
            }
            return Response<NoContent>.Success(StatusCodes.Status201Created);
        }
    }
}
