using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Entity;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Sebrae.Services;
using Sim.Application.Sebrae.Interfaces;
using System.Security.Claims;

namespace Sim.UI.Web.Areas.Sebrae.Pages
{
    [RoleOrClaimAuthorize(Access.Module, "Permission", AccountType.IsAdminGlobal)]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceRae? _appServiceRae;
        private readonly IAppServiceAtendimento? _appServiceAtendimento;

        public Pagination<EAtendimento>? PaginationAtendimentos { get; set; }
        public IndexModel(IAppServiceRae appServiceRae,
        IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceRae = appServiceRae;
            _appServiceAtendimento = appServiceAtendimento;
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

            var _list = await _appServiceRae!.DoListWithoutRae(Src, User.Identity!.Name);

            RegCount = _list!.Count();

            IQueryable<EAtendimento> _atendimentos = _list!.OrderBy(o => o.Data).AsQueryable();

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
            return RedirectToPage("/Index", new { Src = Src });
        }

        public async Task<JsonResult> OnGetPreview(string id)
        {
            return new JsonResult(await _appServiceAtendimento!.GetAsync(new Guid(id)));
        }
    }
}
