using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Text;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Services;
using Sim.UI.Web.Functions;
using Sim.Application.VM;
using Sim.Application.WebService.RFB.Interfaces;

namespace Sim.UI.Web.Pages.Empresa.Novo
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;
        private readonly IServiceRFB _rfb;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IMapper mapper,
            IReceitaWS receitaWS,
            IServiceRFB rfb)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
            _rfb = rfb;
        }

        [BindProperty(SupportsGet = true)]
        public VMEmpresa? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                var _cnpj = id.Mask("##.###.###/####-##");
                var _emp = await _appServiceEmpresa.DoListAsync(s => s.CNPJ == _cnpj);
                var rws = await _receitaWS.ConsultarCPNJAsync(id);
                Input = _mapper.Map<VMEmpresa>(rws);
                StringBuilder _atv_sec = new();

                if (_emp.Any())
                {

                    foreach (var item in _emp)
                    {
                        item.Nome_Empresarial = rws.Nome_Empresarial;
                        item.Nome_Fantasia = rws.Nome_Fantasia;

                        foreach (var at in rws.AtividadePrincipal!)
                        {
                            item.CNAE_Principal = at.Code;
                            item.Atividade_Principal = at.Text;
                            Input.CNAE_Principal = at.Code;
                            Input.Atividade_Principal = at.Text;
                        }

                        _atv_sec.Clear();
                        foreach (var at in rws.AtividadesSecundarias!)
                            _atv_sec.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));

                        Input.Atividade_Secundarias = _atv_sec.ToString().Trim();
                        item.Atividade_Secundarias = _atv_sec.ToString().Trim();

                        item.CEP = rws.Cep;
                        item.Logradouro = rws.Logradouro;
                        item.Bairro = rws.Bairro;
                        item.Municipio = rws.Municipio;
                        item.UF = rws.Uf;
                        item.Telefone = rws.Telefone;
                        item.Email = rws.Email;

                        await _appServiceEmpresa.UpdateAsync(item);
                    }
                    StatusMessage = "Empresa sincronizada e atualizada com sucesso!";
                }
                else
                {

                    foreach (var at in rws.AtividadePrincipal!)
                    {
                        Input.CNAE_Principal = at.Code;
                        Input.Atividade_Principal = at.Text;
                    }

                    _atv_sec.Clear();
                    foreach (var at in rws.AtividadesSecundarias!)
                    {
                        _atv_sec.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
                    }

                    Input.Atividade_Secundarias = _atv_sec.ToString().Trim();
                    await _appServiceEmpresa.AddAsync(_mapper.Map<Empresas>(Input));
                    StatusMessage = "Empresa sincronizada com sucesso!";
                }
                return Page();

            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro: {ex.Message}";
                return RedirectToPage("/Empresa/Index");
            }
        }

    }
}
