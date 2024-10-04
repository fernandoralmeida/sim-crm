using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Entity;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Sebrae.Services;
using Sim.Application.Sebrae.Interfaces;

namespace Sim.UI.Web.Areas.Sebrae.Pages.RAE
{

    [RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
    public class LancadosModel : PageModel
    {
        private readonly IAppServiceRae _appServiceRae;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        public Pagination<EAtendimento>? PaginationAtendimentos { get; set; }
        public LancadosModel(IAppServiceAtendimento appServiceAtendimento,
        IAppServiceRae appServiceRae)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceRae = appServiceRae;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public int RegCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Src { get; set; }
        public async Task OnGetAsync(int Src, int? pag)
        {
            if (Src == 0)
                Src = DateTime.Now.Year;

            var _list = await _appServiceRae.DoListWithRae(Src, User.Identity!.Name);

            RegCount = _list!.Count();

            IQueryable<EAtendimento> _atendimentos = _list!.OrderByDescending(o => o.Data).AsQueryable();

            pag ??= 1;

            var pagesize = 10;

            PaginationAtendimentos = Pagination<EAtendimento>.Create(_atendimentos.AsNoTracking(), pag ?? 1, pagesize);

            if (!PaginationAtendimentos.Any())
            {
                StatusMessage = string.Format("Não há atendimentos para lançar");
            }
        }

        public IActionResult OnPostAsync(int Src)
        {
            return RedirectToPage("./Lancados", new { Src = Src });
        }

        public JsonResult OnGetPreview(string id)
        {
            return new JsonResult(_appServiceAtendimento.GetAsync(new Guid(id)).Result);
        }
    }
}
