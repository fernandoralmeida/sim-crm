using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IMapper _mapper;
        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceInscricao appServiceInscricao,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }
        public async Task OnGetAsync(int id)
        {
            var _event = await _appServiceEvento.DoListAsync(s => s.Codigo == id);            
            Input = _mapper.Map<InputModelEvento>(_event.FirstOrDefault());       
            var _qry = Input.Inscritos!.OrderBy(s => s.Participante!.Nome);
            Input.Inscritos = _qry.ToList();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id, int ide)
        {
            var inscrito = await _appServiceInscricao.GetAsync(id);
            await _appServiceInscricao.RemoveAsync(inscrito); 
            return RedirectToPage("./Index", new { id = ide });
        }

        public async Task OnPostReorder(int id)
        {
            var _event = await _appServiceEvento.DoListAsync(s => s.Codigo == id); 
            Input = _mapper.Map<InputModelEvento>(_event.FirstOrDefault());    
            var _qry = Input.Inscritos!.OrderBy(s => s.Data_Inscricao);
            Input.Inscritos = _qry.ToList();        
        }

        public async Task<JsonResult> OnGetDetalheInscrito(string id)
        {
            return new JsonResult( new List<Domain.Entity.Inscricao>() { await _appServiceInscricao.GetAsync(new Guid(id)) });
        }
    }
}
