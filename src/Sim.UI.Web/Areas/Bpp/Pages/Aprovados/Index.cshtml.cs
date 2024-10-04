using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Domain.BancoPovo.Models;
using Sim.Application.BancoPovo.Functions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages.Aprovados;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel
{

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;

    [BindProperty]
    public IEnumerable<VMContrato>? MeusContratos { get; set; }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public decimal TotalCredito { get; set; }

    [BindProperty]
    public Guid GetID { get; set; }

    [BindProperty]
    public EContrato.EnSituacao GetSituacao { get; set; }

    [BindProperty]
    public EContrato.EnPagamento GetPagamento { get; set; }

    public SelectList? ESituacoes { get; set; }
    public SelectList? EPagamentos { get; set; }

    public IndexModel(IMapper mapper,
        IAppServiceContratos appServiceContratos)
    {
        _mapper = mapper;
        _appcontratos = appServiceContratos;
    }

    private void LoadSelectors()
    {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        EPagamentos = new SelectList(Enum.GetNames(typeof(EContrato.EnPagamento)));
    }
    public async Task OnGetAsync()
    {
        LoadSelectors();
        var _lista = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity!.Name);
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_lista!.Where(s => EContrato.ContratosAprovadosRegulares(s)));
        TotalCredito = MeusContratos.Totalize();
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src)
    {
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity!.Name && s.Situacao == EContrato.EnSituacao.Analise);
        var _list_src = _list!.Where(s => s.Empresa!.Nome_Empresarial!.Contains(src) || s.Cliente!.Nome!.Contains(src));
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list_src);
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostGoSituacaoAsync()
    {
        LoadSelectors();

        var _upsituacao = await _appcontratos.GetIdAsync(GetID);

        _upsituacao!.Situacao = GetSituacao;
        _upsituacao.Pagamento = GetPagamento;
        _upsituacao.DataSituacao = DateTime.Now;

        await _appcontratos.UpdateAsync(_upsituacao);
    }
}

