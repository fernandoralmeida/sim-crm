using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Cliente.Manager
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;

        public IndexModel(IAppServicePessoa pessoa, IMapper mapper)
        {
            _pessoa = pessoa;
            _mapper = mapper;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPessoa? Input { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t = await _pessoa.GetAsync((Guid)id);

            Input = _mapper.Map<InputModelPessoa>(t);

            if (Input.Deficiencia != null)
            {
                if (t.Deficiencia!.Contains("Física"))
                    Input.Fisica = true;

                if (t.Deficiencia!.Contains("Visual"))
                    Input.Visual = true;

                if (t.Deficiencia!.Contains("Auditiva"))
                    Input.Auditiva = true;

                if (t.Deficiencia!.Contains("Intelectual"))
                    Input.Intelectual = true;
            }


            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Alerta: Verifique se todos os campos foram preenchidos corretamente!";
                return Page();
            }


            try
            {
                var pessoa = _mapper.Map<Pessoa>(Input);

                pessoa.Deficiencia = string.Empty;

                if (Input!.Fisica)
                    pessoa.Deficiencia += "Física;";

                if (Input.Visual)
                    pessoa.Deficiencia += "Visual;";

                if (Input.Auditiva)
                    pessoa.Deficiencia += "Auditiva;";

                if (Input.Intelectual)
                    pessoa.Deficiencia += "Intelectual;";

                await _pessoa.UpdateAsync(pessoa);

                return RedirectToPage("/Cliente/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }

        private async Task<Pessoa> PessoaExists(Guid id)
        {
            return await _pessoa.GetAsync(id);
        }

    }
}
