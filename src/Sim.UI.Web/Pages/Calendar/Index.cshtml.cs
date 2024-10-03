using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Agenda.Interfaces;
using Sim.Application.Agenda.Views;
using Sim.Application.Interfaces;
using Sim.Domain.Evento.Model;

namespace Sim.UI.Web.Pages.Calendar;

public partial class CalendarPage : PageModel
{

    private readonly IAppServiceEvento _appServiceEvento;
    private readonly IAppServiceReminder _appServiceReminder;

    public CalendarPage(IAppServiceEvento appServiceEvento,
                        IAppServiceReminder appServiceReminder)
    {
        _appServiceEvento = appServiceEvento;
        _appServiceReminder = appServiceReminder;
    }
    public class Control
    {
        public int Next { get; set; }
        public int Previous { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month;
    }

    public class Calendar
    {
        // Days od Week 
        public string? Month { get; set; }
        public int Year { get; set; }
        public List<KeyValuePair<string, int>>? Months { get; set; } = new();
        public List<int>? Years { get; set; } = new();
        public IEnumerable<string>? Head { get; set; }
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

    public Calendar DoCalendar { get; set; } = new();
    public Control CtrlCalendar { get; set; } = new();
    public SelectList? Acessos { get; set; }

    [BindProperty(SupportsGet = true)]
    public VReminder InputModel { get; set; } = new();

    [DisplayName("Data")]
    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Date)]
    public DateTime InputDate { get; set; } = new();

    [DisplayName("Horário")]
    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Time)]
    public DateTime InputTime { get; set; } = new();

    private async Task ConstructCalendar(int year, int month, IEnumerable<EEvento> eventos, IEnumerable<VReminder> reminds)
    {
        Acessos = new SelectList(Enum.GetNames(typeof(VReminder.TReminder)));

        await Task.Run(() =>
        {
            DoCalendar.Head = new List<string>() { "Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab" };

            for (int i = 1; i <= 12; i++)
            {
                var _m = new DateTime(year, i, 1).ToString("MMMM", new System.Globalization.CultureInfo("pt-BR"));
                DoCalendar.Months!.Add(new(_m.ToUpper(), i));
            }

            for (int i = year - 10; i <= year + 10; i++)
            {
                DoCalendar.Years?.Add(i);
            }

            int _dayweek = (int)new DateTime(year, month, 1).DayOfWeek;
            var _daysmonth = DateTime.DaysInMonth(year, month);

            DoCalendar.Days = new();

            for (int i = 0; i < _dayweek; i++)
            {
                DoCalendar.Days!.Add(new() { Title = "", Events = new() });
            }

            for (int i = 1; i <= _daysmonth; i++)
            {
                var _eventos = new List<Calendar.CalendarDays.Event>();
                foreach (var e in eventos.Where(s => s.Data!.Value.Day == i))
                {
                    _eventos.Add(new() { Id = e.Id, Name = e.Nome, Code = e.Codigo.ToString(), Data = e.Data, IsRemind = false });
                }

                foreach (var r in reminds.Where(s => s.Data!.Day == i))
                {
                    _eventos.Add(new() { Id = r.Id, Name = r.Titulo, Code = r.Id.ToString(), Data = r.Data, IsRemind = true });
                }

                DoCalendar.Days!.Add(new() { Title = i.ToString(), Events = _eventos });
            }

            // for (int i = _daysmonth; i < 35; i++)
            // {
            //     DoCalendar.Days!.Add(new() { Title = "...", Event = new() });
            // }

            DoCalendar.Month = new DateTime(year, month, 1).ToString("MMMM", new System.Globalization.CultureInfo("pt-BR"));
            DoCalendar.Year = year;
            CtrlCalendar.Ano = year;
            CtrlCalendar.Month = month;
            CtrlCalendar.Previous = new DateTime(year, month, 1).AddMonths(-1).Month;
            CtrlCalendar.Next = new DateTime(year, month, 1).AddMonths(+1).Month; ;
        });
    }

    public async Task OnGetAsync(int y, int m)
    {
        var _year = y == 0 ? DateTime.Now.Year : y;
        var _month = m == 0 ? DateTime.Now.Month : m;

        var _list_e = await _appServiceEvento
                                .DoListAsync(s =>
                                    s.Data!.Value.Year == _year &&
                                    s.Data.Value.Month == _month &&
                                    s.Situacao != EEvento.ESituacao.Cancelado);

        var _reminds = await _appServiceReminder.DoListAsync(s =>
                                    s.Data!.Year == _year &&
                                    s.Data.Month == _month &&
                                        (s.Owner == User.Identity!.Name ||
                                         s.Visivel == EReminder.TReminder.Publico));


        await ConstructCalendar(_year, _month, _list_e, _reminds!);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var timeSpan = TimeSpan.Parse(InputTime.ToShortTimeString());
        InputModel.Data = InputDate.Date.Add(timeSpan);
        InputModel.Owner = User.Identity!.Name;
        InputModel.Status = true;        
        await _appServiceReminder.AddNewAsync(InputModel);
        return RedirectToPage("./Index");
    }

}