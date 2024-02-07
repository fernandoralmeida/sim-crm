using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes
{
    [Authorize]
    public class StatusModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;

        public StatusModel(IAppServiceInscricao appServiceInscricao){
            _appServiceInscricao = appServiceInscricao;
        }

        public async Task OnGet(Guid id, int ide, bool st)
        {
            var inscrito = await _appServiceInscricao.GetAsync(id);
            inscrito.Presente = st;
            await _appServiceInscricao.UpdateAsync(inscrito);
        }

    }
}