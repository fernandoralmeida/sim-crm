using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Triagem
{
    
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex>? ListaPAT { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex>? ListaBPP { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex>? ListaSA { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex>? ListaSE { get; set; }

        public class InputModelIndex
        {
            public string? Atendente { get; set; }
            public string? Status { get; set; }
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager,
            IAppServiceAtendimento appAtendimento,
            IAppServiceStatusAtendimento appServiceStatusAtendimento)
        {
            _userManager = userManager;
            _appAtendimento = appAtendimento;
            _appServiceStatusAtendimento = appServiceStatusAtendimento;
        }

        private async Task<IEnumerable<InputModelIndex>> ListUsersAsync(string setor)
        {
            var list = new List<InputModelIndex>();
            var users = await _userManager.GetUsersInRoleAsync(setor);

            foreach (ApplicationUser u in users)
            {
                var t = await _appServiceStatusAtendimento.DoListAsync(i => i.UnserName == u.UserName);

                if(t.Any())
                    if (t.FirstOrDefault()!.Online)
                    {
                        var ativo = await _appAtendimento.DoListAsync(s => s.Owner_AppUser_Id == u.UserName && s.Status == "Ativo");

                        if (ativo.Any())
                            list.Add(new InputModelIndex() { Atendente = u.Name, Status = "Em Atendimento" });

                        else
                            list.Add(new InputModelIndex() { Atendente = u.Name, Status = "Disponível" });
                    }
            }

            return list;
        }

        public async Task OnGet()
        {
            ListaPAT = await ListUsersAsync("M_Pat");
            ListaBPP = await ListUsersAsync("M_BancoPovo");
            ListaSA = await ListUsersAsync("M_Sebrae");
            ListaSE = await ListUsersAsync("M_SalaEmpreendedor");
        }
    }
}
