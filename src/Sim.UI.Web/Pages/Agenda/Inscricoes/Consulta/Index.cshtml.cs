using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes.Consulta
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex? Input { get; set; }

        public class InputModelIndex
        {
            [DisplayName("CPF Participante")]
            public string? GetCPF { get; set; }
            public IEnumerable<InputModelInscricao>? ListaInscricoes { get; set; }
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(IAppServiceInscricao appServiceEvento,
            IMapper mapper)
        {
            _mapper = mapper;
            _appServiceInscricao = appServiceEvento;
        }

        public void OnGet()
        { }

        public async Task OnPostAsync()
        {
            Input!.ListaInscricoes = _mapper.Map<IEnumerable<InputModelInscricao>>
                (await _appServiceInscricao.DoListAsync(s => s.Participante!.CPF == Input.GetCPF));
        }
    }
}
