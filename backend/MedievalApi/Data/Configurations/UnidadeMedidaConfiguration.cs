using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedievalApi.Models;

namespace MedievalApi.Data.Configurations;

public class UnidadeMedidaConfiguration : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        builder.ToTable("unidade_medida");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(u => u.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Sigla)
            .HasColumnName("sigla")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.Tipo)
            .HasColumnName("tipo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Ativo)
            .HasColumnName("ativo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(u => u.CriadoEm)
            .HasColumnName("criado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(u => u.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Índices
        builder.HasIndex(u => u.Sigla)
            .IsUnique()
            .HasDatabaseName("ix_unidade_medida_sigla");

        builder.HasIndex(u => u.Tipo)
            .HasDatabaseName("ix_unidade_medida_tipo");

        builder.HasIndex(u => u.Ativo)
            .HasDatabaseName("ix_unidade_medida_ativo");
    }
}
