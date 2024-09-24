using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Sebrae.Model;

namespace Sim.Data.Config.Entity;

public class SimplesMap : IEntityTypeConfiguration<ESimples>
{
    public void Configure(EntityTypeBuilder<ESimples> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Documento)
            .HasColumnType("varchar(255)");
        builder.Property(c => c.Exercicio)
            .HasColumnType("varchar(255)");
    }
}