using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using Sim.UI.Web.Pages.Empresa.Export;

namespace Sim.UI.Web.Pages.Empresa.Consulta
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _empresaApp;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public SelectList? Municipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel? Input { get; set; }

        public Pagination<Empresas>? PaginationEmpresas { get; set; }
        public int RegCount { get; set; }

        public class InputModel
        {

            [DisplayName("CNPJ")]
            public string? CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string? RazaoSocial { get; set; }
            public string? CNAE { get; set; }
            public string? Logradouro { get; set; }
            public string? Bairro { get; set; }
        }

        public async Task OnGetAsync(string cnpj, string rs, string cnae, string lgd, string bro, int? pag)
        {
            try
            {
                Input!.CNPJ = cnpj;
                Input.RazaoSocial = rs;
                Input.CNAE = cnae;
                Input.Logradouro = lgd;
                Input.Bairro = bro;

                pag ??= 1;

                var _list = await _empresaApp.DoListAsync(s => s.CNPJ!.Contains(cnpj) ||
                                                                s.Nome_Empresarial!.Contains(rs) ||
                                                                s.CNAE_Principal!.Contains(cnae) ||
                                                                //s.Atividade_Principal!.Contains(param) ||
                                                                //s.Atividade_Secundarias!.Contains(param) ||
                                                                s.Logradouro!.Contains(lgd) ||
                                                                s.Bairro!.Contains(bro));

                RegCount = _list.Count();

                var pagesize = 10;

                IQueryable<Empresas> _empresas = _list.AsQueryable();

                PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), pag ?? 1, pagesize);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }


        public async Task OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _list = await _empresaApp.DoListAsync(s => s.CNPJ!.Contains(Input!.CNPJ!) ||
                                                                    s.Nome_Empresarial!.Contains(Input.RazaoSocial!) ||
                                                                    s.CNAE_Principal!.Contains(Input.CNAE!) ||
                                                                    //s.Atividade_Principal!.Contains(param) ||
                                                                    //s.Atividade_Secundarias!.Contains(param) ||
                                                                    s.Logradouro!.Contains(Input.Logradouro!) ||
                                                                    s.Bairro!.Contains(Input.Bairro!));

                    RegCount = _list.Count();

                    var pagesize = 10;

                    IQueryable<Empresas> _empresas = _list.AsQueryable();

                    PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), 1, pagesize);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }

}
