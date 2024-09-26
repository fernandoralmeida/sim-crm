using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Evento.Model;

namespace Sim.Data.Config.Entity
{
    public class ReminderMap : IEntityTypeConfiguration<EReminder>
    {
        public void Configure(EntityTypeBuilder<EReminder> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Titulo)
                .HasColumnType("varchar(255)");
            builder.Property(c => c.Local)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Owner)
                .HasColumnType("varchar(255)");

        }
    }
}
