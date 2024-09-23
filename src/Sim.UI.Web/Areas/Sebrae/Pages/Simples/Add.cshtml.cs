using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Helpers;
using Sim.Domain.Sebrae.Model;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Sebrae.Services;

namespace Sim.UI.Web.Areas.Sebrae.Pages.Simples;

[Authorize(Roles = $"{AccountType.Adm_Global},{Access.Module}")]
public class AddModel : PageModel
{
    private readonly IAppServiceSimples _simples;
    private readonly IAppServiceEmpresa _empresas;
    public bool _result = false;
    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public Empresas? InputEmpresa { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputDocumento { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputExercicio { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputChave { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public IEnumerable<ESimples>? Listar { get; set; }

    public AddModel(IAppServiceSimples simples,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _simples = simples;
        _empresas = empresa;
    }

    public void OnGet() { }
    public async Task<JsonResult> OnGetAddEmpresa(string doc)
    {
        var _result = new List<(Guid id, string document, string nome)>();

        var _con = doc.MaskRemove().Mask("##.###.###/####-##");
        var _list_e = await _empresas.DoListAsync(s => s.CNPJ == _con);
        foreach (var p in _list_e)
        {
            _result.Add((p.Id!, p.CNPJ!, p.Nome_Empresarial!));
        }

        return new JsonResult(_result);
    }

    public async Task OnPostAsync()
        => await Task.Run(async () =>
        {
            var _e = await _empresas.GetAsync(Id);
            var _list = await _simples.DoListAsync(s => s.Empresa!.Id == _e.Id);

            foreach (var item in _list!)
            {
                StatusMessage = $"Alerta: Empresa já tem uma chave do Simples Cadastrada!";
                Listar = await _simples.DoListAsync(s => s.Empresa!.Id == _e.Id);
                _result = true;
                return;
            }

            if (_e != null)
            {
                await _simples.AddAsync(new ESimples()
                {
                    Empresa = _e,
                    Documento = InputDocumento,
                    Exercicio = InputExercicio,
                    Chave = InputChave
                });
                Listar = await _simples.DoListAsync(s => s.Chave == InputChave);
                _result = true;
            }
            else
            {
                StatusMessage = $"Alerta: Empresa não encontrada!";
                _result = false;
                return;
            }
        });


}