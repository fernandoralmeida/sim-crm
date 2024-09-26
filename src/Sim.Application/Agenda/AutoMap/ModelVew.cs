using AutoMapper;
using Sim.Application.Agenda.Views;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Agenda.AutoMap;

public class ModelView : Profile
{
    public ModelView()
    {
        CreateMap<EReminder, VReminder>().ReverseMap();
    }
}