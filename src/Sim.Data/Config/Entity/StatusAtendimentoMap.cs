using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    class StatusAtendimentoMap : IEntityTypeConfiguration<StatusAtendimento>
    {
        public void Configure(EntityTypeBuilder<StatusAtendimento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UnserName)
                .HasColumnType("varchar(256)");
        }
    }
}
