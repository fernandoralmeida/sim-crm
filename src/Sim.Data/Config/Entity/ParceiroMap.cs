using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Evento.Model;

namespace Sim.Data.Config.Entity
{
    public class ParceiroMap : IEntityTypeConfiguration<EParceiro>
    {
        public void Configure(EntityTypeBuilder<EParceiro> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
        }
    }
}
