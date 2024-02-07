using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Customer.Models;

namespace Sim.Data.Config.Entity;

public class BindingsMap : IEntityTypeConfiguration<EBindings>
{
    public void Configure(EntityTypeBuilder<EBindings> builder)
    {
        builder.HasKey(c => c.Id);
    }
}