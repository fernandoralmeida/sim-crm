using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Agenda.Interfaces;
using Sim.Application.Interfaces;
using Sim.Domain.Evento.Model;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CCalendar : ControllerBase
{
    private readonly IAppServiceEvento _eventos;
    private readonly IAppServiceReminder _reminder;
    public CCalendar(IAppServiceEvento eventos,
            IAppServiceReminder reminder)
    {
        _eventos = eventos;
        _reminder = reminder;
    }

    [HttpGet("calendar/{y}/{m}/{d}")]
    public async Task<IActionResult> GetCalendar([FromRoute] int y, [FromRoute] int m, [FromRoute] int d)
    {
        var _events = await _eventos
                        .DoListAsync(s =>
                            s.Data!.Value.Year == y &&
                            s.Data.Value.Month == m &&
                            s.Situacao != EEvento.ESituacao.Cancelado);

        var _reminds = await _reminder.DoListAsync(s =>
                                    s.Data!.Year == y &&
                                    s.Data.Month == m &&
                                        (s.Owner == User.Identity!.Name ||
                                         s.Visivel == EReminder.TReminder.Publico));
        Days = new();

        var _daysmonth = DateTime.DaysInMonth(y, m);


        var _l_eventos = new List<CalendarDays.Event>();
        foreach (var e in _events.Where(s => s.Data!.Value.Day == d))
        {
            _l_eventos.Add(new() { Id = e.Id, Name = e.Nome, Code = e.Codigo.ToString(), Data = e.Data, IsRemind = false });
        }

        foreach (var r in _reminds!.Where(s => s.Data!.Day == d))
        {
            _l_eventos.Add(new() { Id = r.Id, Name = r.Titulo, Code = r.Id.ToString(), Data = r.Data, IsRemind = true });
        }

        Days!.Add(new() { Title = $"Agenda {d}-{m}-{y}", Events = _l_eventos });

        return Ok(Days);
    }

    public List<CalendarDays>? Days { get; set; }
    public class CalendarDays
    {
        public string? Title { get; set; }
        public List<Event>? Events { get; set; } = new();

        public class Event
        {
            public Guid Id { get; set; }
            public string? Code { get; set; }
            public string? Name { get; set; }
            public DateTime? Data { get; set; }
            public bool IsRemind { get; set; } = false;
        }
    }

}