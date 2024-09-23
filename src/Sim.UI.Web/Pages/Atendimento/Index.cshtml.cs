using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
                            IAppServiceSecretaria appSecretaria)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appSecretaria = appSecretaria;
            Input = new()
            {
                DataAtendimento = DateTime.Now.Date
            };
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public InputModel? Input { get; set; }

        public class InputModel
        {
            [DisplayName("Data")]
            [DataType(DataType.Date)]
            public DateTime? DataAtendimento { get; set; }

            public IEnumerable<EAtendimento>? ListaAtendimento { get; set; }
        }

        private async Task LoadAsync(DateTime? date)
        {
            var _dominioativo = await _appSecretaria.DoListAsync(s => s.Acronimo == HttpContext.Session.GetString("Dominio"));
            Input!.DataAtendimento = date;
            Input.ListaAtendimento = await _appServiceAtendimento
                                                .DoListAsync(a =>
                                                    a.Owner_AppUser_Id == User.Identity!.Name
                                                    && a.Data!.Value.Date == date!.Value.Date
                                                    && a.Status == "Finalizado"
                                                    && a.Ativo == true);
            //&& a.Dominio == _dominioativo.FirstOrDefault());
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await LoadAsync(Input!.DataAtendimento);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await LoadAsync(Input!.DataAtendimento);

                if (!Input.ListaAtendimento!.Any())
                {
                    StatusMessage = string.Format("Alerta: Não há atendimentos para {0}", Input.DataAtendimento!.Value.Date);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input!.ListaAtendimento = new List<EAtendimento>();
            }

            return Page();
        }

    }
}
