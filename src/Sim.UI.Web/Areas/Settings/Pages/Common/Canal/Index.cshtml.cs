using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Canal;

[Authorize(Policy = "AdminOrSettings")]
public class IndexModel : PageModel
{
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IAppServiceCanal _appCanal;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IAppServiceCanal appCanal,
        IMapper mapper) {
        _mapper = mapper;
        _appSecretaria = appSecretaria;
        _appCanal = appCanal;        
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMCanal? Input { get; set; }
    
    [BindProperty]
    public EOrganizacao? Unidade { get; set; }
    public IEnumerable<ECanal>? Canais { get; set; }

    private async Task OnLoad(Guid id) {
        Input = new();
        Unidade = await _appSecretaria.GetAsync(id);
        Input.Dominio = Unidade;
        Canais = await _appCanal.DoListAsync(filter: s => s.Dominio!.Id == id || s.Dominio == null);
    }

    public async Task OnGetAsync(string id) {
        await OnLoad(new Guid(id));
    }

    public async Task OnPostAddAsync(Guid id) {
        try {
            Input!.Ativo = true;
            var _dom = await _appSecretaria.GetAsync(id);
            Input.Dominio = _dom;
            if (ModelState.IsValid)  {
                await _appCanal.AddAsync(_mapper.Map<ECanal>(Input));
            }
            await OnLoad(id);
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message;
        }
    }

    public async Task OnPostRemoveAsync(Guid id, Guid domain) {
        try {
            var canal = await _appCanal.GetAsync(id);
            await _appCanal.RemoveAsync(canal);
            await OnLoad(domain);
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}

