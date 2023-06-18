using IdentityService.API.DTOs;
using SharedService.Responses;
using System.Threading.Tasks;

namespace IdentityService.API.Abstractions
{
    public interface IUserService
    {
        Task<Response<NoContent>> CreateUser(CreateUserDTO user);
    }
}
