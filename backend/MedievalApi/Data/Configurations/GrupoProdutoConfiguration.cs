using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class GrupoProdutoConfiguration : IEntityTypeConfiguration<GrupoProduto>
{
    public void Configure(EntityTypeBuilder<GrupoProduto> builder)
    {
        builder.ToTable("grupo_produto");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(g => g.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(255);

        builder.Property(g => g.Ativo)
            .HasColumnName("ativo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(g => g.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(g => g.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasIndex(g => g.Nome)
            .IsUnique()
            .HasDatabaseName("ix_grupo_produto_nome");

        builder.HasIndex(g => g.Ativo)
            .HasDatabaseName("ix_grupo_produto_ativo");
    }
}
