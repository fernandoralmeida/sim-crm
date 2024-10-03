using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Evento.Model;
using Sim.Domain.Organizacao.Model;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Eventos;

[Authorize(Policy = "AdminOrSettings")]
public class IndexModel : PageModel
{
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IAppServiceTipo _appTipo;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceTipo appServiceTipo,
        IMapper mapper) {
        _appSecretaria = appServiceSecretaria;
        _appTipo = appServiceTipo;
        _mapper = mapper;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMTipo? Input { get; set; }

    [BindProperty]
    public EOrganizacao? Unidade { get; set; }
    public IEnumerable<ETipo>? Tipos { get; set; }

    private async Task OnLoad(Guid id) {
        Input = new();
        Unidade = await _appSecretaria.GetAsync(id);
        Input.Dominio = Unidade;
        Tipos = await _appTipo.DoListAsync(filter: s => s.Dominio!.Id == id || s.Dominio == null);
    }

    public async Task OnGetAsync(string id) {
        await OnLoad(new Guid(id));
    }

    public async Task OnPostAddAsync(Guid id) {
        try {
            Input!.Ativo = true;
            var _dom = await _appSecretaria.GetAsync(id);
            Input.Dominio = _dom;
            Input.Tipo = "Evento";
            await _appTipo.AddAsync(_mapper.Map<ETipo>(Input));            
            await OnLoad(id);
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message;
        }
    }

    public async Task OnPostRemoveAsync(Guid id, Guid domain) {
        try
        {
            var canal = await _appTipo.GetAsync(id);
            await _appTipo.RemoveAsync(canal);
            await OnLoad(domain);
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}

