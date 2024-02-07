using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Organizacao.Model;

namespace Sim.Data.Config.Entity
{
    public class ServicoMap : IEntityTypeConfiguration<EServico>
    {
        public void Configure(EntityTypeBuilder<EServico> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");

        }
    }
}
