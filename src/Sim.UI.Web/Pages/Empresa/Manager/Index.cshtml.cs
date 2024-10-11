using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text;
using AutoMapper;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Services;
using Sim.UI.Web.Functions;
using Sim.Application.VM;

namespace Sim.UI.Web.Pages.Empresa.Manager
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        [BindProperty]
        public VMEmpresa? Input { get; set; }

        [BindProperty]
        public string? Cnpj { get; set; }

        [BindProperty]
        public Guid? Id { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        private async Task<VMEmpresa> SyncData(Guid id)
        {
            try
            {
                var _emp = await _appServiceEmpresa.GetAsync(id);
                Cnpj = _emp.CNPJ;
                var rws = await _receitaWS.ConsultarCPNJAsync(_emp.CNPJ!.MaskRemove());
                var _syncEmpresa = _mapper.Map<VMEmpresa>(rws);

                foreach (var at in rws.AtividadePrincipal!)
                {
                    _syncEmpresa.CNAE_Principal = at.Code;
                    _syncEmpresa.Atividade_Principal = at.Text;
                }

                StringBuilder sb = new();
                foreach (var at in rws.AtividadesSecundarias!)
                {
                    sb.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
                }

                _syncEmpresa.Atividade_Secundarias = sb.ToString().Trim();

                _syncEmpresa.Id = id;

                return _syncEmpresa;
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return new();
            }
        }

        public async Task OnGetAsync(Guid id, bool? sync)
        {
            try
            {
                Id = id;

                if (sync == true)
                    Input = await SyncData((Guid)Id!);
                else
                    Input = _mapper.Map<VMEmpresa>(await _appServiceEmpresa.GetAsync(id));

                Cnpj = Input.CNPJ;

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        // public async Task<IActionResult> OnPostSyncAsync()
        // {
        //     try
        //     {
        //         var _input = await LoadAsync(Cnpj!.MaskRemove(), (Guid)Id!);
        //         await _appServiceEmpresa.UpdateAsync(_mapper.Map<Empresas>(_input));
        //         StatusMessage = "Empresa sincronizada com sucesso!";
        //         return RedirectToPage();
        //     }
        //     catch (Exception ex)
        //     {
        //         StatusMessage = "Erro: " + ex.Message;
        //         return Page();
        //     }
        // }

        public async Task OnPostAsync()
        {
            try
            {
                await _appServiceEmpresa.UpdateAsync(_mapper.Map<Empresas>(Input));
                StatusMessage = "Empresa sincronizada e atualizada com sucesso!";
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }
}
