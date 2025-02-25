using Microsoft.AspNetCore.Identity;
using System.Data;

namespace HRMSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
