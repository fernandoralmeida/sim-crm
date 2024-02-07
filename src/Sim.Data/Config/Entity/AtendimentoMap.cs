using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class AtendimentoMap : IEntityTypeConfiguration<EAtendimento>
    {
        public void Configure(EntityTypeBuilder<EAtendimento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Protocolo)
                .HasColumnType("varchar(256)");
            builder.HasIndex(c => c.Protocolo).IsUnique();
            builder.Property(c => c.Protocolo)
                .IsRequired();
            builder.Property(c => c.Setor)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Canal)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Servicos)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Status)
                .HasColumnType("varchar(max)");

        }
    }
}
