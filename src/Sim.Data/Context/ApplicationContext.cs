using Microsoft.EntityFrameworkCore;
using Sim.Domain.Entity;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Evento.Model;
using Sim.Domain.Customer.Models;
using Sim.Domain.Sebrae.Model;

namespace Sim.Data.Context
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<EBindings>? Vinculos { get; set; }
        public DbSet<Empresas>? Empresa { get; set; }
        public DbSet<Pessoa>? Pessoa { get; set; }
        public DbSet<EAtendimento>? Atendimento { get; set; }
        public DbSet<ECanal>? Canal { get; set; }
        public DbSet<EEvento>? Evento { get; set; }
        public DbSet<EParceiro>? Parceiro { get; set; }
        public DbSet<Planner>? Planner { get; set; }
        public DbSet<EOrganizacao>? Secretaria { get; set; }
        public DbSet<EServico>? Servico { get; set; }
        public DbSet<Inscricao>? Inscricao { get; set; }
        public DbSet<ETipo>? Tipos { get; set; }
        public DbSet<Contador>? Contador { get; set; }
        public DbSet<StatusAtendimento>? StatusAtendimento { get; set; }

        //Sebrae Aqui
        public DbSet<RaeSebrae>? Sebrae { get; set; }
        public DbSet<ESimples>? Simples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EBindings>().ToTable("Vinculos");
            modelBuilder.Entity<Pessoa>().ToTable("Pessoa");
            modelBuilder.Entity<Empresas>().ToTable("Empresa");
            modelBuilder.Entity<EAtendimento>().ToTable("Atendimento");
            modelBuilder.Entity<EParceiro>().ToTable("Parceiros");
            modelBuilder.Entity<ECanal>().ToTable("Canal");
            modelBuilder.Entity<EEvento>().ToTable("Evento");
            modelBuilder.Entity<Planner>().ToTable("Planer");
            modelBuilder.Entity<EOrganizacao>().ToTable("Secretaria");
            modelBuilder.Entity<EServico>().ToTable("Servico");
            modelBuilder.Entity<Inscricao>().ToTable("Inscricao");
            modelBuilder.Entity<ETipo>().ToTable("Tipos");
            modelBuilder.Entity<Contador>().ToTable("Protocolos");
            modelBuilder.Entity<StatusAtendimento>().ToTable("StatusAtendimento");

            modelBuilder.Entity<RaeSebrae>().ToTable("RaeSebrae");
            modelBuilder.Entity<ESimples>().ToTable("Simples");

            modelBuilder.ApplyConfiguration(new Config.Entity.AtendimentoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.CanalMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.BindingsMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EmpresaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PessoaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PlannerMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ParceiroMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.SecretariaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ServicoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.InscricaoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EventoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.TipoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ContadorMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.StatusAtendimentoMap());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data_Cadastro") != null))
            {

                if (entry.State == EntityState.Added)
                    entry.Property("Data_Cadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("Data_Cadastro").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Ultima_Alteracao") != null))
            {
                entry.Property("Ultima_Alteracao").CurrentValue = DateTime.Now;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
