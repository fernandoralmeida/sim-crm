using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Planner
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly IAppServicePlaner _planner;
        private readonly IMapper _mapper;

        public IndexModel(IAppServicePlaner planner,
            IMapper mapper)
        {

            _planner = planner;
            _mapper = mapper;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPlanner? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? InicioSemana { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? FimSemana { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? MeuDia { get; set; }

        public async Task OnGetAsync()
        {
            MeuDia = DateTime.Now.Date;
            PlannerTimer(DateTime.Now);

            var plnn = await _planner.DoListAsync(s => 
                                            s.DataInicial!.Value.Date == Input!.DataInicial
                                            && s.DataFinal!.Value.Date == Input.DataFinal
                                            && s.Owner_AppUser_Id == User.Identity!.Name);

            if (!plnn.Any())
            {
                Input!.Owner_AppUser_Id = User.Identity!.Name;
                Input.Ativo = true;
                await _planner.AddAsync(_mapper.Map<Domain.Entity.Planner>(Input));
            }

            foreach (Domain.Entity.Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }
        }

        public async Task OnPostAsync()
        {
            PlannerTimer(DateTime.Now.Date);

            await _planner.UpdateAsync(_mapper.Map<Domain.Entity.Planner>(Input));

            var plnn = await _planner.DoListAsync(s => 
                                            s.DataInicial!.Value.Date == Input!.DataInicial
                                            && s.DataFinal!.Value.Date == Input.DataFinal
                                            && s.Owner_AppUser_Id == User.Identity!.Name);

            foreach (Domain.Entity.Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }
        }

        public async Task<IActionResult> OnPostToDateAsync()
        {

            PlannerTimer((DateTime)MeuDia!);

            var plnn = await _planner.DoListAsync(s => 
                                            s.DataInicial!.Value.Date == Input!.DataInicial
                                            && s.DataFinal!.Value.Date == Input.DataFinal
                                            && s.Owner_AppUser_Id == User.Identity!.Name);

            if (!plnn.Any())
            {
                Input = new()
                {
                    Owner_AppUser_Id = User.Identity!.Name,
                    Ativo = true
                };
                await _planner.AddAsync(_mapper.Map<Domain.Entity.Planner>(Input));
            }

            foreach (Domain.Entity.Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }

            return Page();
        }

        private void PlannerTimer(DateTime dia)
        {
            var dia_da_semana = dia.DayOfWeek;
            InicioSemana = dia.Date;
            FimSemana = dia.Date;

            switch (dia_da_semana)
            {
                case DayOfWeek.Sunday:
                    InicioSemana = dia.AddDays(0);
                    FimSemana = dia.AddDays(6);
                    break;

                case DayOfWeek.Monday:
                    InicioSemana = dia.AddDays(-1);
                    FimSemana = dia.AddDays(5);
                    break;

                case DayOfWeek.Tuesday:
                    InicioSemana = dia.AddDays(-2);
                    FimSemana = dia.AddDays(4);
                    break;

                case DayOfWeek.Wednesday:
                    InicioSemana = dia.AddDays(-3);
                    FimSemana = dia.AddDays(3);
                    break;

                case DayOfWeek.Thursday:
                    InicioSemana = dia.AddDays(-4);
                    FimSemana = dia.AddDays(2);
                    break;

                case DayOfWeek.Friday:
                    InicioSemana = dia.AddDays(-5);
                    FimSemana = dia.AddDays(1);
                    break;

                case DayOfWeek.Saturday:
                    InicioSemana = dia.AddDays(-6);
                    FimSemana = dia.AddDays(0);
                    break;

                default:
                    break;
            }

            Input!.DataInicial = InicioSemana.Value.Date;
            Input.DataFinal = FimSemana.Value.Date;
        }
    }
}
