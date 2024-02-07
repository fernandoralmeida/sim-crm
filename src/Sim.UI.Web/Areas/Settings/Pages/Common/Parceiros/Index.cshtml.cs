using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Evento.Model;
using Sim.Domain.Organizacao.Model;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Parceiros;

[Authorize(Roles = $"{AccountType.Adm_Global},{AccountType.Adm_Settings}")]
public class IndexModel : PageModel
{

    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IAppServiceParceiro _appParceiro;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceParceiro appServiceParceiro,
        IMapper mapper)
    {
        _appSecretaria = appServiceSecretaria;
        _appParceiro = appServiceParceiro;
        _mapper = mapper;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMParceiros? Input { get; set; }
    
    [BindProperty]
    public EOrganizacao? Unidade { get; set; }
    public IEnumerable<EParceiro>? Parceiros { get; set; }

    private async Task OnLoad(Guid id) {
        Input = new();
        Unidade = await _appSecretaria.GetAsync(id);
        Input.Dominio = Unidade;
        Parceiros = await _appParceiro.DoListAsync(filter: s => s.Dominio!.Id == id || s.Dominio == null);
    }

    public async Task OnGetAsync(string id)
    {
        await OnLoad(new Guid(id));
    }

    public async Task OnPostAddAsync(string id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Input!.Dominio = await _appSecretaria.GetAsync(new Guid(id));
                Input.Ativo = true;
                await _appParceiro.AddAsync(_mapper.Map<EParceiro>(Input));
            }
            await OnLoad(new Guid(id));
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }

    public async Task OnPostRemoveAsync(Guid id, string domain)
    {
        try
        {
            var canal = await _appParceiro.GetAsync(id);

            await _appParceiro.RemoveAsync(canal);

            await OnLoad(new Guid(domain));
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}

