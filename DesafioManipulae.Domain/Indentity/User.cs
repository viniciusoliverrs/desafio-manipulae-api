using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DesafioManipulae.Domain.Indentity
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FullName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}