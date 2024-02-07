using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Organizacao.Model;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Setor;

[Authorize(Roles = $"{AccountType.Adm_Global},{AccountType.Adm_Settings}")]
public class IndexModel : PageModel
{
    //private readonly IAppServicePrefeitura _appPrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    //private readonly IAppServiceSetor _appSetor;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        //_appSetor = appSetor;
        _appSecretaria = appSecretaria;
        //_appPrefeitura = appPrefeitura;
        _mapper = mapper;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMSecretaria? Input { get; set; }

    [BindProperty]
    public VMSecretaria? Unidade { get; set; }
    public SelectList? Hierarquia { get; set; }
    public IEnumerable<EOrganizacao>? Setores { get; set; }

    private async Task OnLoad(Guid id) {
        var _list = await _appSecretaria.DoListAsync();
        Setores = await _appSecretaria.DoListHierarquia2from1Async(_list, id);
        Unidade = _mapper.Map<VMSecretaria>(await _appSecretaria.GetAsync(id));  
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x == "Setor"));
    }

    public async Task OnGetAsync(string id) {
        await OnLoad(new Guid(id));
    }

    public async Task<IActionResult> OnPostAsync() {
        try{
            Input!.Ativo = true;     
            Input.Dominio = Unidade!.Id;
            Input.Acronimo = Input.Nome;           
            await _appSecretaria.AddAsync(_mapper.Map<EOrganizacao>(Input));
            await OnLoad(Unidade.Id);                
        }
        catch(Exception ex) {
            StatusMessage = "Erro: " +  ex.Message;
        }
        return Page();  
    }

    public async Task OnGetRemove(string id, Guid dm) {
        try
        {   
            var _unidade = await _appSecretaria.GetAsync(new Guid(id));         
            await _appSecretaria.RemoveAsync(_unidade);    
            await OnLoad(dm); 
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}


