using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Sim.Identity.Policies;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    [Authorize(Policy = PolicyExtensions.IsAdminSettings)]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceSecretaria _appServicePrefeitura;
        private readonly IMapper _mapper;
        public IndexModel(IAppServiceSecretaria appService,
            IMapper mapper)
        {
            _appServicePrefeitura = appService;
            _mapper = mapper;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMSecretaria? Input { get; set; }
        public IEnumerable<VMSecretaria>? Listar { get; set; }
        public SelectList? Hierarquia { get; set; }

        private async Task OnLoad()
        {
            var t = await _appServicePrefeitura.DoListHierarquia0Async(await _appServicePrefeitura.DoListAsync());
            Listar = _mapper.Map<IEnumerable<VMSecretaria>>(t);
            Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x == "Matriz"));
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            // if (User.IsInRole(AccountType.Adm_Settings))
            // {
            //     var _dominioativo = HttpContext.Session.GetString("Dominio");
            //     var _dominioID = await _appServicePrefeitura.DoListAsync(s => s.Acronimo == _dominioativo);

            //     return RedirectToPage("/Common/Setor/Index", new { id = _dominioID.FirstOrDefault()?.Id });
            // }
            await OnLoad();
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            try
            {
                Input!.Ativo = true;
                await _appServicePrefeitura.AddAsync(_mapper.Map<EOrganizacao>(Input));
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluir!" + "\n" + ex.Message;
            }

            return Page();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var sec = await _appServicePrefeitura.GetAsync(id);
                await _appServicePrefeitura.RemoveAsync(sec);

                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover Prefeitura!" + "\n" + ex.Message;
            }
        }

        public async Task OnGetRemove(Guid id)
        {
            try
            {
                var canal = await _appServicePrefeitura.GetAsync(id);
                await _appServicePrefeitura.RemoveAsync(canal);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        public async Task OnGetStatus(Guid id, bool st)
        {
            var _org = await _appServicePrefeitura.GetAsync(id);
            _org.Ativo = st;
            await _appServicePrefeitura.UpdateAsync(_org);
        }
    }
}
