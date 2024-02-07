using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade.Manage;

[Authorize( Roles = $"{AccountType.Adm_Global},{AccountType.Adm_Settings}")]
public class IndexModel : PageModel
{
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        _appSecretaria = appSecretaria;
        _mapper = mapper;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMSecretaria? Input { get; set; }
    public SelectList? Hierarquia { get; set; }

    private void OnLoad() {        
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x == "Secretaria")); 
    }

    public async Task OnGetAsync(string id)    {
       OnLoad();  
       Input = _mapper.Map<VMSecretaria>(await _appSecretaria.GetAsync(new Guid(id)));  
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {                
            if(Input!.Hierarquia == EHierarquia.Secretaria) {
                var _org = await _appSecretaria.DoListHierarquia0Async(await _appSecretaria.DoListAsync());
                Input.Dominio = _org.SingleOrDefault()!.Id;
            }
            await _appSecretaria.UpdateAsync(_mapper.Map<EOrganizacao>(Input));
            OnLoad();
            StatusMessage = "Informações atualizadas com sucesso!";
        }
    }
}