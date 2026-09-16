using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedievalApi.Models;

namespace MedievalApi.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categoria");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(255);

        builder.Property(c => c.Ativo)
            .HasColumnName("ativo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(c => c.CriadoEm)
            .HasColumnName("criado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(c => c.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Índices
        builder.HasIndex(c => c.Nome)
            .IsUnique()
            .HasDatabaseName("ix_categoria_nome");

        builder.HasIndex(c => c.Ativo)
            .HasDatabaseName("ix_categoria_ativo");
    }
}
