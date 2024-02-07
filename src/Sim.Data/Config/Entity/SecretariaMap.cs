using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Organizacao.Model;

namespace Sim.Data.Config.Entity
{
    public class SecretariaMap : IEntityTypeConfiguration<EOrganizacao>
    {
        public void Configure(EntityTypeBuilder<EOrganizacao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Acronimo)
                .HasColumnType("varchar(max)");
        }
    }
}
