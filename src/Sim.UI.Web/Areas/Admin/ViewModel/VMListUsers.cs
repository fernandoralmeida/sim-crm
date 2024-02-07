using System.ComponentModel;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{

    public class VMListUsers
    {
        [DisplayName("Procurar por Id")]
        public string? GetUserName { get; set; }

        public IEnumerable<ApplicationUser>? Users { get; set; }

    }
}
