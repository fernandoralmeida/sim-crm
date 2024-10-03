
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Identity.Config;
using Sim.UI.Web.Areas.Sebrae.Services;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Areas.Sebrae.Pages.RAE;

[RoleOrClaimAuthorize(Access.Module, "Permission", AccountType.IsAdminGlobal)]
public class ExportModel : PageModel
{
    private readonly IAppServiceAtendimento _repository;
    private readonly IAppServiceRae _sebrae;
    public Pagination<EAtendimento>? PaginationAtendimentos { get; set; }
    public ExportModel(IAppServiceAtendimento repository,
    IAppServiceRae sebrae)
    {
        _repository = repository;
        _sebrae = sebrae;
    }
    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Date)]
    public DateTime DataI { get; set; } = new DateTime(DateTime.Now.Year, 01, 01);

    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Date)]
    public DateTime DataF { get; set; } = DateTime.Today;

    public IEnumerable<EAtendimento>? Atendimentos { get; set; } = new List<EAtendimento>();

    [BindProperty(SupportsGet = true)]
    public int Src { get; set; }
    public void OnGetAsync()
    { }

    public async Task OnPostAsync()
    {
        Atendimentos = await _repository.DoListAsync(s => s.Data!.Value.Date >= DataI.Date && s.Data.Value.Date <= DataF.Date && s.Setor == Access.Module);
    }

    public async Task<IActionResult> OnPostExportToFile()
    {
        Atendimentos = await _repository.DoListAsync(s => s.Data!.Value.Date >= DataI.Date && s.Data.Value.Date <= DataF.Date && s.Setor == Access.Module);
        var _file = await _sebrae.DoExport(Atendimentos, User.Identity!.Name!);
        return File(_file.StreamFile, _file.ContentType, _file.Name);
    }

    public JsonResult OnGetPreview(string id)
    {
        return new JsonResult(_repository.DoListAsync(s => s.Id == new Guid(id)).Result);
    }
}