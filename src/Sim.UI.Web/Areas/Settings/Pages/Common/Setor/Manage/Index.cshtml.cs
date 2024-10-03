using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Setor.Manage;

[Authorize(Policy = "AdminOrSettings")]
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
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x == "Setor")); 
    }

    public async Task OnGetAsync(string id, string dm)    {
        OnLoad();  
        Input = _mapper.Map<VMSecretaria>(await _appSecretaria.GetAsync(new Guid(id)));  
        Input.Dominio = new Guid(dm);
        if(Input.Hierarquia == EHierarquia.Setor) {                        
            if(string.IsNullOrEmpty(Input.Acronimo)) Input.Acronimo = Input.Nome;
        }
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid) {
            if(Input!.Hierarquia == EHierarquia.Setor) {                        
                Input.Acronimo = Input.Nome;
            }
            await _appSecretaria.UpdateAsync(_mapper.Map<EOrganizacao>(Input));
            OnLoad();
            StatusMessage = "Informações atualizadas com sucesso!";
        }
    }
}