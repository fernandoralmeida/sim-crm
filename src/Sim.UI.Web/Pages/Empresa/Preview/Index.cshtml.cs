using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using AutoMapper;
using Sim.Application.VM;

namespace Sim.UI.Web.Pages.Empresa.Preview;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceEmpresa _empresaApp;
    private readonly IMapper _mapper;

    public IndexModel(IAppServiceEmpresa appServiceEmpresa,
        IMapper mapper)
    {
        _empresaApp = appServiceEmpresa;
        _mapper = mapper;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public VMEmpresa? Input { get; set; }

    public async Task OnGetAsync(Guid id) =>
        Input = _mapper.Map<VMEmpresa>(await _empresaApp.GetAsync(id)); 


}

