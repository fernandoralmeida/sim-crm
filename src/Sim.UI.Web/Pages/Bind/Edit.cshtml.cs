using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Customer.Interfaces;
using Sim.Application.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.UI.Web.Pages.Bind;

[Authorize]
public class EditModel : PageModel
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
    public Guid EditID { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? NomeEmpresarial { get; set; }
    public SelectList? Vinculos { get; set; }

    [BindProperty]
    public TBindings VinculoSelecionado { get; set; }

    public IEnumerable<EBindings>? Listar { get; set; }

    public EditModel(IAppServiceBindings repository,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _bindings = repository;
        _pessoas = pessoa;
        _empresas = empresa;
    }

    public async Task OnGetAsync(Guid id)
    {
        Vinculos = new SelectList(Enum.GetValues(typeof(TBindings)));

        foreach (var input in await _bindings.DoListAsync(s => s.Id == id))
        {
            EditID = id;
            Id2 = input.Empresa!.Id;
            GetCNPJ = input.Empresa!.CNPJ;
            GetCPF = input.Pessoa!.CPF;
            Id1 = input.Pessoa!.Id;
            Nome = input.Pessoa.Nome;
            NomeEmpresarial = input.Empresa.Nome_Empresarial;
            VinculoSelecionado = input.Vinculo;
        }
    }

    public async Task OnPostAsync()
    {
        Vinculos = new SelectList(Enum.GetNames(typeof(TBindings)));
        var _edit = await _bindings.GetAsync(EditID);
        var _vinculo = _edit.Vinculo;
        _edit.Vinculo = VinculoSelecionado;
        await _bindings.UpdateAsync(_edit);

        Listar = await _bindings.DoListAsync(s => s.Id == EditID);

        foreach(var item in Listar)
        {
            if (item.Vinculo != _vinculo)
            {
                StatusMessage = "Vinculo atualizado com sucesso!";
                _result = true;
            }
        }

    }
}