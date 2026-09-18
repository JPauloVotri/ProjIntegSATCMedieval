using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
{
    public void Configure(EntityTypeBuilder<Estoque> builder)
    {
        builder.ToTable("estoque");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.ProdutoId)
            .HasColumnName("produto_id")
            .IsRequired();

        builder.Property(e => e.QuantidadeDisponivel)
            .HasColumnName("quantidade_disponivel")
            .HasPrecision(14, 3)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(e => e.ValorTotal)
            .HasColumnName("valor_total")
            .HasPrecision(16, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(e => e.UltimaEntrada)
            .HasColumnName("ultima_entrada")
            .HasColumnType("timestamptz");

        builder.Property(e => e.UltimaSaida)
            .HasColumnName("ultima_saida")
            .HasColumnType("timestamptz");

        builder.Property(e => e.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasOne(e => e.Produto)
            .WithOne(p => p.Estoque)
            .HasForeignKey<Estoque>(e => e.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_estoque_produto");

        // Índices
        builder.HasIndex(e => e.ProdutoId)
            .IsUnique()
            .HasDatabaseName("ix_estoque_produto_id");

        builder.HasIndex(e => e.QuantidadeDisponivel)
            .HasDatabaseName("ix_estoque_quantidade_disponivel");
    }
}
