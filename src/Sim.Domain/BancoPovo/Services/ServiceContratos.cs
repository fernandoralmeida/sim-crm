using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Domain.BancoPovo.Services;

public class ServiceContratos : ServiceBase<EContrato>, IServiceContratos
{
    private readonly IRepositoryContratos _contratos;

    public ServiceContratos(IRepositoryContratos repositorycontratos) : base(repositorycontratos)
    {
        _contratos = repositorycontratos;
    }

    public async Task<IEnumerable<EContrato>?> DoListAsync(Expression<Func<EContrato, bool>>? filter = null)
    {
       return await _contratos.DoListAsync(filter);
    }

    public async Task<EContrato?> GetIdAsync(Guid id)
    {
        return await _contratos.GetIdAsync(id);
    }
}