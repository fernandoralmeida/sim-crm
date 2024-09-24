using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Functions;
using Sim.Domain.BancoPovo.Models;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages;

[Authorize(Roles = $"{AccountType.Adm_Global},{Access.Module}")]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;

    [BindProperty]
    public IEnumerable<VMContrato>? MeusContratos { get; set; }

    [BindProperty]
    public Guid GetID { get; set; }

    [BindProperty]
    public EContrato.EnSituacao GetSituacao { get; set; }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public decimal TotalCredito { get; set; }
    public SelectList? ESituacoes { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _appcontratos = appServiceContratos;        
    }
    private void LoadSelectors() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
    }
    public async Task OnGetAsync() {        
        LoadSelectors();
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User!.Identity!.Name && s.Situacao == EContrato.EnSituacao.Analise);
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list!);   
        TotalCredito = MeusContratos.Totalize();
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src) {
        LoadSelectors();
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity!.Name && s.Situacao == EContrato.EnSituacao.Analise);
        var _lit_src = _list!.Where(s => s.Empresa!.Nome_Empresarial!.Contains(src) || s.Cliente!.Nome!.Contains(src));
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_lit_src); 
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostGoSituacaoAsync() {
        LoadSelectors();

        var _upsituacao = await _appcontratos.GetIdAsync(GetID);

        _upsituacao!.Situacao = GetSituacao;
        _upsituacao.DataSituacao = DateTime.Now;
        _upsituacao.Pagamento = EContrato.EnPagamento.Regular;

        await _appcontratos.UpdateAsync(_upsituacao);
    }
}

