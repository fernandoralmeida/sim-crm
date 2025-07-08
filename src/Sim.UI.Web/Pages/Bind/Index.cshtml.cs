using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Customer.Models;
using Sim.Application.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Sim.UI.Web.Pages.Bind;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceBindings _bindings;

    public bool _result = false;

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }
    public int StartNumber { get; set; }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; }

    public int NextPage { get; set; }
    public int PreviousPage { get; set; }
    public int RegCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)RegCount / 10);
    public IEnumerable<EBindings>? Listar { get; set; }

    public IndexModel(IAppServiceBindings repository)
    {
        _bindings = repository;
    }

    public async Task OnGetAsync(int pg = 1)
    {
        pg = pg < 1 ? 1 : pg;
        var _list = await _bindings.DoListAsync();
        Listar = _list?.Skip((pg - 1) * 10).Take(10);
        StartNumber = (pg - 1) * 10 + 1;
        CurrentPage = pg;
        NextPage += pg == TotalPages ? pg : pg + 1;
        PreviousPage += pg == 1 ? 1 : pg - 1;
        RegCount = _list?.Count() ?? 0;
    }

    public async Task OnPostAsync()
        => Listar = await _bindings.DoListAsync(s => s.Pessoa!.CPF == Search || s.Empresa!.CNPJ == Search);

    public async Task<JsonResult> OnGetDelete(Guid id)
    {
        var _result = await _bindings.DoListAsync(s => s.Id == id);
        await _bindings.RemoveAsync(await _bindings.GetAsync(id));
        return new JsonResult(_result);
    }
}