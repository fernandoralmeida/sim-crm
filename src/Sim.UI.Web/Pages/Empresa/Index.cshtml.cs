using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using Microsoft.EntityFrameworkCore;
using Sim.Application.WebService.RFB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Empresa
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _empresaApp;
        private readonly IServiceRFB _rfb;
        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IServiceRFB rfb)
        {
            _empresaApp = appServiceEmpresa;
            _rfb = rfb;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public SelectList? Municipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Src { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string? GetDocument { get; set; }

        [Required]
        [BindProperty(SupportsGet = true)]
        public string? GetCaptcha { get; set; }

        public string? Captcha { get { return _rfb.Captcha(); } }

        public Pagination<Empresas>? PaginationEmpresas { get; set; }
        public int RegCount { get; set; }
        public async Task OnGetAsync(string? Src, int? pag)
        {
            try
            {
                pag ??= 1;

                var _list = await _empresaApp.DoListAsync(s => s.CNPJ!.Contains(Src!) ||
                                                                s.Nome_Empresarial!.Contains(Src!) ||
                                                                s.CNAE_Principal!.Contains(Src!) ||
                                                                s.Atividade_Principal!.Contains(Src!) ||
                                                                s.Atividade_Secundarias!.Contains(Src!) ||
                                                                s.Logradouro!.Contains(Src!) ||
                                                                s.Bairro!.Contains(Src!));

                RegCount = _list.Count();

                var pagesize = 10;

                var _empresas = _list.AsQueryable();

                PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), pag ?? 1, pagesize);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        public IActionResult OnPostAddNew() =>
            RedirectToPage("/Empresa/Novo/Index", new { id = GetDocument!.MaskRemove() });

    }
}
