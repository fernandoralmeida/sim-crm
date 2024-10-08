using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Functions;
using Sim.Domain.BancoPovo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;
using static Sim.Domain.BancoPovo.Models.EContrato;

namespace Sim.UI.Web.Areas.Bpp.Pages.Inadimplentes;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;
    private readonly IAppServiceRenegociacoes _apprenegociar;

    [BindProperty]
    public IEnumerable<VMContrato>? MeusContratos { get; set; }

    [BindProperty]
    public decimal TotalCredito { get; set; }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public Guid? GetID { get; set; }

    [BindProperty]
    public EnSituacao? GetSituacao { get; set; }

    [BindProperty]
    public EnPagamento? GetPagamento { get; set; }

    public SelectList? ESituacoes { get; set; }
    public SelectList? EPagamentos { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceRenegociacoes appServiceRenegociacoes,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _apprenegociar = appServiceRenegociacoes;
            _appcontratos = appServiceContratos;        
    }
    private void LoadSelectors() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        EPagamentos = new SelectList(Enum.GetNames(typeof(EContrato.EnPagamento)));
    }
    public async Task OnGetAsync() {      
        LoadSelectors();  
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity!.Name && s.Situacao == EContrato.EnSituacao.Aprovado && s.Pagamento == EContrato.EnPagamento.Inadimplente);
        MeusContratos =  _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list!);     
        TotalCredito = MeusContratos.Totalize();
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src) {
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity!.Name && s.Situacao == EContrato.EnSituacao.Analise);
        var _list_src = _list!.Where(s => s.Empresa!.Nome_Empresarial!.Contains(src) || s.Cliente!.Nome!.Contains(src));
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list_src);
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostGoSituacaoAsync() {
        LoadSelectors();

        var _upsituacao = await _appcontratos.GetIdAsync((Guid)GetID!);

        _upsituacao!.Situacao = (EnSituacao)GetSituacao!;
        _upsituacao.Pagamento = (EnPagamento)GetPagamento!;
        _upsituacao.DataSituacao = DateTime.Now;

        await _appcontratos.UpdateAsync(_upsituacao);
    }

    public async Task OnGetGoRenegociarAsync(Guid id) {
        
        var _upsituacao = await _appcontratos.GetIdAsync(id);

        _upsituacao!.Situacao = EnSituacao.Aprovado;
        _upsituacao.Pagamento = EnPagamento.Regular;
        _upsituacao.DataSituacao = DateTime.Now;

        var _renegociar = new ERenegociacoes(){
            Id = new Guid(),
            Contrato = _upsituacao,
            Data = DateTime.Now,
            Situacao = ERenegociacoes.EnSituacao.Ativo
        };       
        await _appcontratos.UpdateAsync(_upsituacao);
        await _apprenegociar.AddAsync(_renegociar);
    }
}

