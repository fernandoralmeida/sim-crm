using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresas>
    {
        public void Configure(EntityTypeBuilder<Empresas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.CNPJ).IsUnique();
            builder.Property(c => c.CNPJ)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Data_Abertura);

            builder.Property(c => c.Nome_Empresarial)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Nome_Fantasia)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.CNAE_Principal)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Atividade_Principal)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Atividade_Secundarias)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.CEP)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Logradouro)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Numero)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Bairro)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Municipio)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.UF)
                .HasColumnType("varchar(2)");

            builder.Property(c => c.Telefone)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Situacao_Cadastral)
                .HasColumnType("varchar(max)");

        }
    }
}
