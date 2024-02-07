using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryAtendimento : RepositoryBase<EAtendimento>, IRepositoryAtendimento
    {
        public RepositoryAtendimento(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null)
        {
            return await _db.Atendimento!
                                .Where(filter ?? (p => true))
                                //.Where(a => a.Status == "Finalizado")
                                //.Where(a => a.Ativo == true)
                                .Include(p => p.Pessoa)
                                .Include(e => e.Empresa)
                                .OrderBy(o => o.Data)
                                .AsNoTrackingWithIdentityResolution()
                                .ToListAsync();
        }

        public async Task<EAtendimento> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Atendimento!
                            .Include(p => p.Pessoa)
                            .Include(e => e.Empresa)
                            .Where(i => i.Id == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam)
        {
            var d1 = lparam[0].ToString();
            var d2 = lparam[1].ToString();
            _ = d1!.Replace("-", "/");
            _ = d2!.Replace("-", "/");

            var dataI = Convert.ToDateTime(d1);
            var dataF = Convert.ToDateTime(d2);
            var cpf = lparam[2] != null ? (string)lparam[2] : "";
            var nome = lparam[3] != null ? (string)lparam[3] : "";
            var cnpj = lparam[4] != null ? (string)lparam[4] : "";
            var razaosocial = lparam[5] != null ? (string)lparam[5] : "";
            var cnae = lparam[6] != null ? (string)lparam[6] : "";
            var servico = lparam[7] != null ? (string)lparam[7] : "";
            var user = lparam[8] != null ? (string)lparam[8] : "";
            var setor = lparam[9] != null ? (string)lparam[9] : "";

            return await _db.Atendimento!
                                .Include(p => p.Pessoa)
                                .Include(e => e.Empresa)
                                .Where(a => a.Data >= dataI && a.Data <= dataF)
                                .Where(a => a.Status == "Finalizado" && a.Ativo == true)
                                .Where(a => a.Pessoa!.CPF!.Contains(cpf))
                                .Where(a => a.Pessoa!.Nome!.Contains(nome))
                                .Where(a => a.Empresa!.CNPJ!.Contains(cnpj))
                                .Where(a => a.Empresa!.Nome_Empresarial!.Contains(razaosocial))
                                .Where(a => a.Empresa!.CNAE_Principal!.Contains(cnae))
                                .Where(a => a.Servicos!.Contains(servico))
                                .Where(a => a.Owner_AppUser_Id!.Contains(user))
                                .Where(a => a.Setor!.Contains(setor))
                                .AsNoTrackingWithIdentityResolution()
                                .OrderBy(o => o.Data)
                                .ToListAsync();             
        }

    }
}
