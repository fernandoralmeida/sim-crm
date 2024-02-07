using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class PlannerMap : IEntityTypeConfiguration<Planner>
    {
        public void Configure(EntityTypeBuilder<Planner> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Segunda)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Terca)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Quarta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Quinta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Sexta)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Sabado)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.ProximaSemana)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Prioridades)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Anotacao)
                .HasColumnType("varchar(max)");
        }
    }
}
