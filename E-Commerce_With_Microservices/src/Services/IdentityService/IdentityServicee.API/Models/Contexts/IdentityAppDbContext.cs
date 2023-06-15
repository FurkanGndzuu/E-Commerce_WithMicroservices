using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServicee.API.Models.Contexts
{
    public class IdentityAppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base()
        {

        }
    }
}
