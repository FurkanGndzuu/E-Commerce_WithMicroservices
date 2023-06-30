using IdentityService.API.DTOs;
using System.Threading.Tasks;

namespace IdentityService.API.Abstractions
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserDTO user);
        Task<string> FindEmail(string userId);


    }
}
