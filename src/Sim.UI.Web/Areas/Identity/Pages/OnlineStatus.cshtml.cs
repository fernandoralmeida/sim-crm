using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Identity.Pages
{
    [Authorize]
    public class OnlineStatusModel : PageModel
    {
        private readonly IAppServiceStatusAtendimento _statusAtendimento;

        public OnlineStatusModel(IAppServiceStatusAtendimento statusAtendimento)
        {
            _statusAtendimento = statusAtendimento;
        }

        public async Task<IActionResult> OnGet(string id, bool val)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if(new Guid(id) == Guid.Empty) {
                    var meustatus = new StatusAtendimento(){ 
                        Id = new Guid(),
                        UnserName = User.Identity!.Name,
                        Online = true
                    };
                    _statusAtendimento.AddAsync(meustatus).Wait();
                } else {
                    var meustatus = await _statusAtendimento.GetAsync(new Guid(id));
                    meustatus.Online = val;
                    _statusAtendimento.UpdateAsync(meustatus).Wait();
                }                
            }

            return RedirectToPage();
        }

    }
}
