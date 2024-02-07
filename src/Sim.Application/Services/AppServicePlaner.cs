using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServicePlaner : AppServiceBase<Planner>, IAppServicePlaner
    {
        private readonly IServicePlaner _planer;
        public AppServicePlaner(IServicePlaner planer)
            : base(planer)
        { _planer = planer; }

        public async Task<IEnumerable<Planner>> DoListAsync(Expression<Func<Planner, bool>>? filter = null)
        {
            return await _planer.DoListAsync(filter);
        }

        public async Task<Planner> GetAsync(Guid id)
        {
            return await _planer.GetAsync(id);
        }
    }
}
