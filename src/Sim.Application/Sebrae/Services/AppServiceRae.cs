using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;
using Sim.Domain.Sebrae.Model;
using Sim.Domain.Helpers;
using Sim.Application.Sebrae.Views;
using Sim.Application.Helpers;
using System.IO;
using Sim.Domain.Sebrae.Interfaces;

namespace Sim.Application.Sebrae.Services;
public class AppServiceRae : IAppServiceRae
{
    protected readonly IServiceRae? _serviceRae;

    public AppServiceRae(IServiceRae serviceRae)
    {
        _serviceRae = serviceRae;
    }

    public async Task AddNewRaeAsync(EAtendimento obj)
    {
        await _serviceRae!.AddAsync(obj);
    }

    public async Task<(MemoryStream StreamFile, string ContentType, string Name)> DoExport(IEnumerable<EAtendimento> atendimentos, string user)
    {
        var _list_to_export = new List<RExportRAE>();
        var _count = 0;
        foreach (var at in atendimentos)
        {
            _list_to_export.Add(new RExportRAE
            {
                Count = _count++,
                Data = $"{at.Data!.Value:yyyy-MM-dd}",
                Cliente = at.Pessoa?.Nome?.ToUpper(),
                Servicos = at.Servicos,
                Canal = at.Canal ?? "",
                Lancamento = $"{at.Ultima_Alteracao!.Value:yyyy-MM-dd}",
                NumeroRAE = at.Sebrae?.RAE,
                Atendente = at.Owner_AppUser_Id
            });

        }

        return await ExportFile.ToExcel(_list_to_export, $"lista-atend-sebrae-rae-{user}-{DateTime.Now:yyyyMMddHHmmss}");

        //return File(_file.StremFile, _file.ContentType, _file.Name);
    }

    public async Task<IEnumerable<EAtendimento>?> DoListWithoutRae(int ano, string? username = null)
    {
        return await _serviceRae!
                        .DoListAsync(s => s.Sebrae == null &&
                                        s.Anonimo == false &&
                                        s.Status == "Finalizado" &&
                                        s.Setor == "Sebrae Aqui" &&
                                        s.Owner_AppUser_Id == username &&
                                        s.Data!.Value.Year == ano);
    }
    public async Task<IEnumerable<EAtendimento>?> DoListWithRae(int ano, string? username = null)
    {
        return await _serviceRae!
                        .DoListAsync(s => s.Sebrae != null &&
                                        s.Anonimo == false &&
                                        s.Status == "Finalizado" &&
                                        s.Setor == "Sebrae Aqui" &&
                                        s.Owner_AppUser_Id == username &&
                                        s.Data!.Value.Year == ano);
    }

    public async Task RemoveRaeAsync(RaeSebrae obj)
    {
        await _serviceRae!.RemoveAsync(obj);
    }

    public async Task UpdateRaeAsync(RaeSebrae obj)
    {
        await _serviceRae!.UpdateAsync(obj);
    }
}