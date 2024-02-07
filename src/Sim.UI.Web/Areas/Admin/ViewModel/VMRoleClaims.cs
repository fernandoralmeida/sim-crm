using System.ComponentModel;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    public class VMRoleClaims
    {
        public int Id { get; set; }
        
        [DisplayName("Role Id")]
        public string? RoleId { get; set; }

        [DisplayName("Claim Tipo")]
        public string? ClaimType { get; set; }

        [DisplayName("Claim Value")]
        public string? ClaimValue { get; set; }

    }
}
