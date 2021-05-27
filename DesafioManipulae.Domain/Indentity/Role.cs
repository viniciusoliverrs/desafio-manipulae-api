using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DesafioManipulae.Domain.Indentity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}