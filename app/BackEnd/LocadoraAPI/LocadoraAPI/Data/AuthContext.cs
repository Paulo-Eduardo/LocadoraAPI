using Microsoft.AspNet.Identity.EntityFramework;

namespace LocadoraAPI.Data
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}
