using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Model;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Sebrae.Services;

namespace Sim.UI.Web.Areas.Sebrae.Pages.Simples;

[RoleOrClaimAuthorize(Access.Module, "Permission", AccountType.IsAdminGlobal)]
public class EditModel : PageModel
{
    private readonly IAppServiceSimples _simples;
    private readonly IAppServiceEmpresa _empresas;
    public bool _result = false;
    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputDocumento { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputExercicio { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? InputChave { get; set; }

    [BindProperty(SupportsGet = true)]
    public Empresas? InputEmpresa { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public IEnumerable<ESimples>? Listar { get; set; }

    public EditModel(IAppServiceSimples simples,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _simples = simples;
        _empresas = empresa;
    }

    public async Task OnGetAsync(Guid id)
    {
        var _list = await _simples.DoListAsync(s => s.Id == id);
        foreach (var input in _list!)
        {
            Id = id;
            InputEmpresa = input.Empresa;
            InputDocumento = input.Documento;
            InputExercicio = input.Exercicio;
            InputChave = input.Chave;
        }
    }

    public async Task OnPostAsync()
    {
        var _edit = await _simples.GetByIdAsync(Id);

        _edit!.Documento = InputDocumento;
        _edit.Exercicio = InputExercicio;
        _edit.Chave = InputChave;

        await _simples.UpdateAsync(_edit);

        Listar = await _simples.DoListAsync(s => s.Id == Id);
        StatusMessage = "Simples atualizado com sucesso!";
        _result = true;
    }
}