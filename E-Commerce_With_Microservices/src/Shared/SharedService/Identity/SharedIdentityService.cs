using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SharedService.Identity
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserIdAsync() => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirst("email").Value;
        public (string, string) GetUserNameAndSurName() => (_httpContextAccessor.HttpContext.User.FindFirst("given_name").Value ,
            _httpContextAccessor.HttpContext.User.FindFirst("Surname").Value);
    }
}
