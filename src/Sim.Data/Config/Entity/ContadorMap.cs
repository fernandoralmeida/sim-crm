using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class ContadorMap : IEntityTypeConfiguration<Contador>
    {
        public void Configure(EntityTypeBuilder<Contador> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .HasColumnType("varchar(max)");

            builder
                .Property(c => c.Modulo)
                .HasColumnType("varchar(max)");

            builder
                .Property(c => c.AppUserId)
                .HasColumnType("nvarchar(max)");
        }

    }


}
