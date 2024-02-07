using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Planner
{
    [Authorize]
    public class ListarModel : PageModel
    {
        [TempData]
        public string? StatusMessage { get; set; }
        public void OnGet()
        {
        }
    }
}
