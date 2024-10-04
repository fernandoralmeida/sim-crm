using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.Interfaces;
using Sim.Domain.BancoPovo.Models;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages.Edit;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel
{   
    private readonly IMapper _mapper;
    private readonly IAppServiceEmpresa _appServiceEmpresa;
    private readonly IAppServicePessoa _appServicePessoa;
    private readonly IAppServiceContratos _appContratos;

    [TempData]
    public string? StatusMessage { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public VMContrato? InputContrato { get; set; }

    [DisplayName("CNPJ")]
    [BindProperty]
    public string? GetCNPJ { get; set; }

    [DisplayName("CPF")]
    [BindProperty]
    public string? GetCPF { get; set; }

    public SelectList? ESituacoes { get; set; }
    public SelectList? EPagamentos { get; set; }

    public IndexModel(IMapper mapper,
        IAppServiceEmpresa appempresa,
        IAppServicePessoa apppessoa,
        IAppServiceContratos appServiceContratos)
    {
        _mapper = mapper;
        _appServiceEmpresa = appempresa;
        _appServicePessoa = apppessoa;
        _appContratos = appServiceContratos;
    }

    private void LoadSelectors() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        EPagamentos = new SelectList(Enum.GetNames(typeof(EContrato.EnPagamento)));
    }
    public async Task OnGet(Guid id) {
        LoadSelectors();
        InputContrato = _mapper.Map<VMContrato>(await _appContratos.GetIdAsync(id));
        InputContrato.UltimaAlteracao = DateTime.Now;
    }
    public async Task OnPostPFAsync() {
        LoadSelectors();
        var lp = await _appServicePessoa.DoListAsync(c => c.CPF == GetCPF);

        if(lp.Count() == 0)
            StatusMessage = "Erro: Cliente não encontrado!";

        foreach(var p in lp)
            InputContrato!.Cliente = p;              
    }

    public async Task OnPostPJAsync() {
        LoadSelectors();
        var le = await _appServiceEmpresa.DoListAsync(c => c.CNPJ == GetCNPJ);

        if(le.Count() == 0)
            StatusMessage = "Erro: Empresa não encontrada!";

        foreach(var e in le)
            InputContrato!.Empresa = e;
    }

    public IActionResult OnPostRemovePF() {
        LoadSelectors();
        InputContrato!.Cliente = null;
        return RedirectToPage("/BancodoPovo/Edit/Index");
    }

    public void OnPostRemovePJ() {
        LoadSelectors();
        InputContrato!.Empresa = null;
    }

    public async Task<IActionResult> OnPostSaveAsync(){
        try{
            LoadSelectors();
            if (InputContrato!.Cliente == null && InputContrato.Empresa == null) {
                StatusMessage = "Erro: Verifique se os campos foram preenchidos corretamente!";
                return Page();
            }
            
            var _contrato = _mapper.Map<EContrato>(InputContrato);            
            
            if(InputContrato.Cliente!= null)
                _contrato.Cliente = await _appServicePessoa.GetAsync(InputContrato.Cliente.Id);
            
            if(InputContrato.Empresa!= null)
                _contrato.Empresa = await _appServiceEmpresa.GetAsync(InputContrato.Empresa.Id);            

            await _appContratos.UpdateAsync(_contrato);

            return RedirectToPage("/Index");
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message + "\n" + ex.Data + "\n" + ex.Source + "\n" + ex.InnerException;
            return Page();
        }
    }

}

