using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages.Contracts;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel
{
    private readonly IAppServiceContratos _appcontratos;

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMReports? InputView { get; set; }

    public IndexModel(IAppServiceContratos appServiceContratos)
    {
        _appcontratos = appServiceContratos;
    }

    public async Task OnGetAsync()
    {
        InputView = new();
        InputView.DataInicial = new DateTime(year: DateTime.Now.Year, month: 1, day: 1);
        InputView.DataFinal = DateTime.Now;
        var _list = await _appcontratos.DoListAsync(s => s.Data >= InputView.DataInicial && s.Data <= InputView.DataFinal);
        InputView!.Relatorios = await _appcontratos.DoReportsAsync(_list!);
    }

    public async Task OnPostAsync()
    {
        var _list = await _appcontratos.DoListAsync(s => s.Data >= InputView!.DataInicial && s.Data <= InputView.DataFinal);
        InputView!.Relatorios = await _appcontratos.DoReportsAsync(_list!);
    }

    public async Task OnPostPreviewAsync(int? id)
    {
        try
        {
            var _list = await _appcontratos.DoListAsync(s => s.Data >= InputView!.DataInicial && s.Data <= InputView.DataFinal);
            InputView!.Relatorios = await _appcontratos.DoReportsAsync(_list!);
            //InputView.DataInicial =  datai;
            //InputView.DataFinal = dataf;
            switch (id)
            {
                case 0:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosEmAnalise(s));
                    break;
                case 1:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosAprovadosRegulares(s));
                    break;
                case 2:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosAprovadosInadimplente(s));
                    break;
                case 3:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosLiquidados(s));
                    break;
                case 4:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosReprovados(s));
                    break;
                case 5:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosCancelados(s));
                    break;
                case 6:
                    InputView.Relatorios!.ListaContratos = _list!.Where(s => Domain.BancoPovo.Models.EContrato.ContratosRenegociados(s));
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }

    }
}

