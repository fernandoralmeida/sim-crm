using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServicePlaner : ServiceBase<Planner>, IServicePlaner
    {
        private readonly IRepositoryPlaner _planer;
        public ServicePlaner(IRepositoryPlaner repositoryPlaner)
            : base(repositoryPlaner)
        { _planer = repositoryPlaner; }

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
