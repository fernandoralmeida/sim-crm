using System.ComponentModel;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    public class VMUserClaims
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        [DisplayName("Claim Tipo")]
        public string? ClaimType { get; set; }

        [DisplayName("Claim Valor")]
        public string? ClaimValue{ get; set; }

        public string? StatusMessage { get; set; }

    }
}
