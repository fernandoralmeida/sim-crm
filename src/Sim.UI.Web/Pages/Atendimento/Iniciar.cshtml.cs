using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.UI.Web.Functions;
using Sim.Application.Customer.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.UI.Web.Pages.Atendimento
{
    [Authorize]
    public class IniciarModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceContador _appServiceContador;
        private readonly IAppServiceBindings _bindings;

        public IniciarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceContador appServiceContador,
            IAppServiceBindings appServiceBindings)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceContador = appServiceContador;
            _bindings = appServiceBindings;
        }

        [DisplayName("CPF")]
        [BindProperty]
        public string? GetCPF { get; set; }

        [DisplayName("CNPJ")]
        [BindProperty]
        public string? GetCNPJ { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento? Input { get; set; }

        public bool HasBind = false;

        [TempData]
        public string? StatusMessage { get; set; }

        private async Task<string> GetProtoloco()
        {
            return await _appServiceContador.GetProtocoloAsync(User.Identity!.Name!, "Atendimento");
        }
        private async Task<Pessoa> GetPessoa(Guid id)
        {
            return await _appServicePessoa.GetAsync(id);
        }
        private async Task<Empresas?> GetEmpresa(string cnpj)
        {
            var emp = await _appServiceEmpresa.DoListAsync(c => c.CNPJ == cnpj);
            StatusMessage = emp.Any() ? string.Empty : "Erro: Empresa não cadastrada!";
            return emp.Any() ? emp!.FirstOrDefault()! : null;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            Input = new();

            var at = await _appServiceAtendimento.DoListAsync(s => s.Owner_AppUser_Id ==  User.Identity!.Name && s.Status == "Ativo");

            if (at.Any())
            {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";
                return RedirectToPage("/Atendimento/Novo/Index");
            }

            if (id != null)
            {
                Input.Pessoa = await GetPessoa((Guid)id);

                foreach (var e in await _bindings.DoListAsync(s => s.Pessoa!.Id == id))
                    Input.Empresa = e.Empresa;

                if (Input.Empresa != null)
                    if (Input.Empresa.Situacao_Cadastral == "BAIXADA")
                    {
                        StatusMessage = "Erro: Empresa encontra-se BAIXADA!";
                        Input.Empresa = null;
                    }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostIncluirEmpresaAsync()
        {
            Input!.Pessoa = await GetPessoa(Input.Pessoa!.Id);
            Input.Empresa = await GetEmpresa(GetCNPJ!);

            if (Input.Empresa != null)
            {
                var _result = await _bindings.DoListAsync(s => s.Pessoa!.Id == Input.Pessoa.Id && s.Empresa!.Id == Input.Empresa.Id);
                if (_result.Count() < 1)
                    HasBind = true;
            }

            return Page();
        }

        public async Task OnPostRemoverEmpresaAsync()
        {
            Input!.Empresa = null;
            Input.Pessoa = await GetPessoa(Input.Pessoa!.Id);
        }

        public async Task OnPostBindingsAsync(Guid pid, Guid eid)
        {
            var _e = await _appServiceEmpresa.GetAsync(eid);
            var _p = await _appServicePessoa.GetAsync(pid);
            await _bindings.AddAsync(new EBindings() { Empresa = _e, Pessoa = _p, Vinculo = TBindings.Proprietario });
            foreach (var intem in await _bindings.DoListAsync(s => s.Pessoa!.Id == _p.Id && s.Empresa!.Id == _e.Id))
            {
                StatusMessage = "Vinculo realizado com sucesso!";
                HasBind = false;
            }
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            try
            {
                if (Input!.Pessoa == null)
                {
                    return Page();
                }

                var atendimento = new EAtendimento()
                {
                    Protocolo = await GetProtoloco(),
                    Data = DateTime.Now,
                    Status = "Ativo",
                    Anonimo = false,
                    Ativo = true,
                    Ultima_Alteracao = DateTime.Now,
                    Owner_AppUser_Id = User.Identity!.Name,
                    Pessoa = await _appServicePessoa.GetAsync(Input.Pessoa.Id),
                    Empresa = Input.Empresa == null ? null : await _appServiceEmpresa.GetAsync(Input.Empresa.Id)
                };

                await _appServiceAtendimento.AddAsync(atendimento);

                StatusMessage = "Lembre-se de incentivar os clientes a responderem nossa pesquisa de satisfação!";

                return RedirectToPage("/Atendimento/Novo/Index");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }

    }
}
