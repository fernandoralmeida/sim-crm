using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Customer.Interfaces;
using Sim.Application.Interfaces;
using Sim.Domain.Customer.Models;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Bind;

[Authorize]
public class AddModel : PageModel
{
    private readonly IAppServiceBindings _bindings;
    private readonly IAppServicePessoa _pessoas;
    private readonly IAppServiceEmpresa _empresas;

    public bool _result = false;

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? GetCPF { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? GetCNPJ { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Nome { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id1 { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id2 { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? NomeEmpresarial { get; set; }
    public SelectList? Vinculos { get; set; }

    [BindProperty(SupportsGet = true)]
    public TBindings VinculoSelecionado { get; set; }

    public IEnumerable<EBindings>? Listar { get; set; }

    public AddModel(IAppServiceBindings repository,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _bindings = repository;
        _pessoas = pessoa;
        _empresas = empresa;
    }

    public async Task OnGetAsync()
        => await Task.Run(() =>
        {
            Vinculos = new SelectList(Enum.GetValues(typeof(TBindings)));
        });

    public async Task<JsonResult> OnGetAddPessoa(string doc)
    {
        var _result = new List<(Guid id, string document, string nome)>();

        var _con = doc.MaskRemove().Mask("###.###.###-##");
        foreach (var p in await _pessoas.DoListAsync(s=> s.CPF == _con))
        {
            _result.Add((p.Id!, p.CPF!, p.Nome!));
        }

        return new JsonResult(_result);
    }

    public async Task<JsonResult> OnGetAddEmpresa(string doc)
    {
        var _result = new List<(Guid id, string document, string nome)>();

        var _con = doc.MaskRemove().Mask("##.###.###/####-##");
        foreach (var p in await _empresas.DoListAsync(c => c.CNPJ == _con))
        {
            _result.Add((p.Id!, p.CNPJ!, p.Nome_Empresarial!));
        }

        return new JsonResult(_result);
    }

    public async Task OnPostAsync()
        => await Task.Run(async () =>
        {
            Vinculos = new SelectList(Enum.GetNames(typeof(TBindings)));
            var _e = await _empresas.GetAsync(Id2);
            var _p = await _pessoas.GetAsync(Id1);

            if (_p != null && _e != null)
            {

                if (_e.Situacao_Cadastral == "BAIXADA")
                {
                    StatusMessage = $"Alerta: Empresa {_e.CNPJ} está {_e.Situacao_Cadastral}!";
                    return;
                }

                var _validate = await _bindings.DoListAsync(s => s.Pessoa!.Id == _p.Id && s.Empresa!.Id == _e.Id);

                if (_validate.Count() > 0)
                {
                    foreach (var item in _validate)
                        StatusMessage = $"Alerta: Já existe um vinculo entre CNPJ {item.Empresa!.CNPJ} e CPF {item.Pessoa!.CPF} => {item.Vinculo}!";

                    _result = true;
                    Listar = _validate;
                    return;
                }

                await _bindings.AddAsync(new EBindings() { Id = new Guid(), Empresa = _e, Pessoa = _p, Vinculo = VinculoSelecionado });
                Listar = await _bindings.DoListAsync(s => s.Pessoa!.Id == _p.Id && s.Empresa!.Id == _e.Id);
                if (Listar.Count() > 0)
                    StatusMessage = "Vinculo realizado com sucesso!";

                _result = true;
                GetCNPJ = string.Empty;
                GetCPF = string.Empty;
                Nome = string.Empty;
                NomeEmpresarial = string.Empty;
            }
            else
            {
                StatusMessage = $"Alerta: Empresa ou Pessoa não encontrada!";
                return;
            }
        });


}