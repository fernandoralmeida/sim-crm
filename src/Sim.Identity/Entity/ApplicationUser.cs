using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Sim.Identity.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Theme { get; set; }

    }
}
