using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.Identity
{
    public interface ISharedIdentityService
    {
       public string GetUserIdAsync();
        public string GetUserEmail();
        public (string, string) GetUserNameAndSurName();
    }
}
