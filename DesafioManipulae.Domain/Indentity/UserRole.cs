using Microsoft.AspNetCore.Identity;

namespace DesafioManipulae.Domain.Indentity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}