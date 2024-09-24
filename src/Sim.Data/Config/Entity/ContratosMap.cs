using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Data.Config.Entity;

public class ContratosMap : IEntityTypeConfiguration<EContrato>
{
    public void Configure(EntityTypeBuilder<EContrato> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Valor)
            .HasColumnType("decimal(18,2)");
        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(255)");
        builder.Property(c => c.AppUser)
            .HasColumnType("varchar(255)");
    }
}