using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Evento.Model;

namespace Sim.Data.Config.Entity
{
    public class TipoMap : IEntityTypeConfiguration<ETipo>
    {
        public void Configure(EntityTypeBuilder<ETipo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Tipo)
                .HasColumnType("varchar(max)");
        }
    }
}
