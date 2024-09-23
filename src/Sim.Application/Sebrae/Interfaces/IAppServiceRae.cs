using Sim.Domain.Sebrae.Model;
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;
using System.Linq.Expressions;
using Sim.Application.Sebrae.Views;

namespace Sim.Application.Sebrae.Interfaces;

public interface IAppServiceRae
{

    Task<IEnumerable<EAtendimento>?> DoListWithRae(int ano, string? username = null);
    Task<IEnumerable<EAtendimento>?> DoListWithoutRae(int ano, string? username = null);
    Task AddNewRaeAsync(EAtendimento obj);
    Task RemoveRaeAsync(RaeSebrae obj);
    Task UpdateRaeAsync(RaeSebrae obj);
    Task<(MemoryStream StreamFile, string ContentType, string Name)> DoExport(IEnumerable<EAtendimento> atendimentos, string user);
}