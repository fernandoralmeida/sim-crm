using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Data.Config.Entity;

public class RenegociacoesMap : IEntityTypeConfiguration<ERenegociacoes>
{
    public void Configure(EntityTypeBuilder<ERenegociacoes> builder)
    {
        builder.HasKey(c => c.Id);
    }
}