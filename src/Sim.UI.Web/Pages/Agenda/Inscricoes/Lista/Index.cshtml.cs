using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes.Lista
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSecretaria _appsecretaria;
        private readonly IMapper _mapper;
        public IndexModel(IAppServiceEvento appServiceEvento,
                            IMapper mapper,
                            IAppServiceSecretaria appsecretaria)
        {
            _appServiceEvento = appServiceEvento;
            _mapper = mapper;
            _appsecretaria = appsecretaria;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public string? Organizacao { get; set; }
        public async Task OnGetAsync(int id)
        {
            var _event = await _appServiceEvento.DoListAsync(s => s.Codigo == id);
            Input = _mapper.Map<InputModelEvento>(_event.FirstOrDefault());
            var _qry = Input.Inscritos!.OrderBy(s => s.Participante!.Nome);
            Input.Inscritos = _qry.ToList();
            foreach (var item in await _appsecretaria.DoListAsync(s => s.Hierarquia == Domain.Organizacao.Model.EHierarquia.Matriz))
                Organizacao = item.Nome;
        }
    }
}
