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
    }
}
