using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Sebrae.Services;

namespace Sim.UI.Web.Areas.Sebrae.Pages.RAE
{
    [Authorize(Roles = $"{AccountType.Adm_Global},{Access.Module}")]
    public class LancarModel : PageModel
    {
        private readonly IAppServiceRae _appServiceRae;
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public LancarModel(IAppServiceAtendimento appServiceAtendimento,
        IAppServiceRae appServiceRae)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceRae = appServiceRae;
        }

        [BindProperty(SupportsGet = true)]
        public Sim.UI.Web.Pages.Atendimento.InputModelAtendimento? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Ano { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? OnEdit { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int y, Guid id, bool? edit)
        {
            Ano = y;
            var atendimento = await _appServiceAtendimento.GetAsync(id);
            OnEdit = edit;
            TempData["OnEdit"] = OnEdit;

            Input = new()
            {
                Id = atendimento.Id,
                Protocolo = atendimento.Protocolo,
                Data = atendimento.Data,
                DataF = atendimento.DataF,
                Setor = atendimento.Setor,
                Canal = atendimento.Canal,
                Servicos = atendimento.Servicos,
                Descricao = atendimento.Descricao,
                Status = atendimento.Status,
                Ultima_Alteracao = atendimento.Ultima_Alteracao,
                Ativo = atendimento.Ativo,
                Owner_AppUser_Id = atendimento.Owner_AppUser_Id,
                Pessoa = atendimento.Pessoa,
                Empresa = atendimento.Empresa,
                Sebrae = atendimento.Sebrae
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                // Recupera o valor de OnEdit do TempData
                OnEdit = TempData["OnEdit"] as bool?;
                
                if (!ModelState.IsValid)
                {
                    // Remover os erros de validação das propriedades que você deseja ignorar
                    ModelState.Remove("Input.Sebrae.Id");
                    ModelState.Remove("Input.Sebrae.RAE");

                    // Revalidar o ModelState após remover os erros específicos
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }
                }

                var _at_rae = new EAtendimento()
                {
                    Id = Input!.Id,
                    Protocolo = Input.Protocolo,
                    Data = Input.Data,
                    DataF = Input.DataF,
                    Setor = Input.Setor,
                    Canal = Input.Canal,
                    Servicos = Input.Servicos,
                    Descricao = Input.Descricao,
                    Status = Input.Status,
                    Ultima_Alteracao = Input.Ultima_Alteracao,
                    Ativo = Input.Ativo,
                    Owner_AppUser_Id = Input.Owner_AppUser_Id,
                    Pessoa = Input.Pessoa,
                    Empresa = Input.Empresa,
                    Sebrae = Input.Sebrae
                };

                await (OnEdit == true
                            ? _appServiceRae.UpdateRaeAsync(_at_rae.Sebrae!)
                            : _appServiceRae.AddNewRaeAsync(_at_rae));

                return (OnEdit == false
                            ? RedirectToPage("/Index", new { Src = Ano })
                            : RedirectToPage("/Rae/Lancados", new { Src = Ano }));
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }
    }
}
