using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Agenda.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using Sim.Application.Agenda.Views;

    public class AppServiceReminder : IAppServiceReminder
    {
        private readonly IServiceReminder _reminder;
        private readonly IMapper _mapper;
        public AppServiceReminder(
                    IServiceReminder reminder,
                    IMapper mapper)
        {
            _reminder = reminder;
            _mapper = mapper;
        }

        public async Task AddNewAsync(VReminder obj)
        {
            await _reminder.AddAsync(_mapper.Map<EReminder>(obj));
        }

        public async Task<IEnumerable<VReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null)
        {
            return _mapper.Map<IEnumerable<VReminder>>(await _reminder.DoListAsync(filter));
        }

        public async Task<VReminder?> GetAsync(Guid id)
        {
            return _mapper.Map<VReminder>(await _reminder.GetAsync(id));
        }

        public async Task RemoveAsync(VReminder obj)
        {
            await _reminder.RemoveAsync(_mapper.Map<EReminder>(obj));
        }

        public async Task UpdateAsync(VReminder obj)
        {
            await _reminder.UpdateAsync(_mapper.Map<EReminder>(obj));
        }
    }
}
