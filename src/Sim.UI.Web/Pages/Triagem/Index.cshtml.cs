using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Application.Interfaces;
using System.Security.Claims;

namespace Sim.UI.Web.Pages.Triagem
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;
        private readonly IAppServiceSecretaria _appsercretaria;
        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>>>? Atendentes { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager,
            IAppServiceAtendimento appAtendimento,
            IAppServiceStatusAtendimento appServiceStatusAtendimento,
            IAppServiceSecretaria appsercretaria)
        {
            _userManager = userManager;
            _appAtendimento = appAtendimento;
            _appServiceStatusAtendimento = appServiceStatusAtendimento;
            _appsercretaria = appsercretaria;
        }

        private async Task ListUsersAsync()
        {
            var _dominios = await _appsercretaria.DoListAsync();

            var _locais = _dominios.Where(s => s.Hierarquia == Domain.Organizacao.Model.EHierarquia.Setor).GroupBy(g => g.Dominio);

            var _list = new List<KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>>>();

            foreach (var sec in _locais)
            {
                var _sec = _dominios.FirstOrDefault(s => s.Id == sec.Key)!.Acronimo ?? "";
                var _setores = new List<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>();
                foreach (var setor in sec)
                {
                    //var _claim = new Claim(setor.Acronimo!, setor.Id.ToString());
                    var users = await _userManager.GetUsersInRoleAsync(setor.Acronimo);
                    var _atendentes = new List<KeyValuePair<string, string>>();

                    foreach (ApplicationUser u in users)
                    {
                        var _atendente = new KeyValuePair<string, string>();
                        foreach (var _any in await _appServiceStatusAtendimento.DoListAsync(i => i.UnserName == u.UserName))
                            if (_any.Online)
                            {
                                var ativo = await _appAtendimento.DoListAsync(s => s.Owner_AppUser_Id == u.UserName && s.Status == "Ativo");

                                if (ativo.Any())
                                    _atendente = new KeyValuePair<string, string>(u.Name!, "Em Atendimento");

                                else
                                    _atendente = new KeyValuePair<string, string>(u.Name!, "Disponível");

                                _atendentes.Add(_atendente);
                            }

                    }
                    _setores.Add(new KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>(setor.Acronimo!, _atendentes));
                }
                _list.Add(new KeyValuePair<string, IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>>(_sec, _setores));
            }

            Atendentes = _list;
        }

        public async Task OnGet()
        {
            await ListUsersAsync();
        }
    }
}
