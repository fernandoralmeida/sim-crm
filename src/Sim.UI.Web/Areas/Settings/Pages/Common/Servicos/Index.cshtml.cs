using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.VM;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using Microsoft.AspNetCore.Authorization;
using Sim.Identity.Policies;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Servicos;

[Authorize(Policy = PolicyExtensions.IsAdminSettings)]
public class IndexModel : PageModel
{
    private readonly IAppServiceServico _appservicos;
    private readonly IAppServiceSecretaria _appdominio;
    private readonly IMapper _mapper;

    public IndexModel(IAppServiceServico appservicos,
        IAppServiceSecretaria appdominio,
        IMapper mapper) {
            _appservicos = appservicos;
            _appdominio = appdominio;
            _mapper = mapper;        
    }

    [TempData]
    public string? StatusMessage { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public VMServicos? Input { get; set; }

    [BindProperty]
    public Guid ReturnID { get; set; }
    
    [BindProperty]
    public Guid SetorID { get; set; }
    [BindProperty]
    public Guid SetDominio { get; set; }
    public IEnumerable<EServico>? Servicos { get; set; }
    public SelectList? Dominios { get; set; }
    public SelectList? AllDominios { get; set; }
    
    public async Task OnLoadAsync(Guid id, Guid dm) {
        var _list = await _appdominio.DoListAsync();
        var _setores = await _appdominio.DoListAsync(s => s.Id == id);
        var _all_setores = await _appdominio.DoListAsync(s => s.Id == dm || s.Dominio == dm);
        Dominios = new SelectList(_setores, nameof(EOrganizacao.Id), nameof(EOrganizacao.Acronimo));
        AllDominios = new SelectList(_all_setores, nameof(EOrganizacao.Id), nameof(EOrganizacao.Acronimo));
        ReturnID = dm;
        SetorID = id;
        Servicos = await _appservicos.DoListAsync(s => s.Dominio!.Id == id || s.Dominio == null);
    }

    public async Task OnGetAsync(string id, string dm) {
        await OnLoadAsync(new Guid(id), new Guid(dm));
    }

    public async Task OnPostAsync() {
        var _dominio = await _appdominio.GetAsync(SetDominio);
        Input!.Id= new Guid();
        Input.Ativo = true;
        Input.Dominio = _dominio;
        if (ModelState.IsValid) {}
            await _appservicos.AddAsync(_mapper.Map<EServico>(Input));
            await OnLoadAsync(SetorID, ReturnID);
            StatusMessage = "Novo serviço incluído com sucesso!";        
    }

    public async Task OnGetManager(string id, string dominio, string nome) {
        try {
            var _servico = await _appservicos.GetAsync(new Guid(id));
            var _dominio = await _appdominio.GetAsync(new Guid(dominio));
            _servico.Dominio = _dominio;
            _servico.Nome = nome;
            await _appservicos.UpdateAsync(_servico);
            StatusMessage = "Serviço alterado com sucesso!";
        }
        catch (Exception ex){
            StatusMessage = "Erro: " + ex.Message;
        }
    }
    public async Task OnGetRemove(Guid id, Guid dm, Guid st) {
        try
        {   
            var _serv = await _appservicos.GetAsync(id);         
            await _appservicos.RemoveAsync(_serv);   
            await OnLoadAsync(st, dm);
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}

