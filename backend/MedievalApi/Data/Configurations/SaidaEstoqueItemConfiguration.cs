using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class SaidaEstoqueItemConfiguration : IEntityTypeConfiguration<SaidaEstoqueItem>
{
    public void Configure(EntityTypeBuilder<SaidaEstoqueItem> builder)
    {
        builder.ToTable("saida_estoque_item");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(si => si.SaidaEstoqueId)
            .HasColumnName("saida_estoque_id")
            .IsRequired();

        builder.Property(si => si.ProdutoId)
            .HasColumnName("produto_id")
            .IsRequired();

        builder.Property(si => si.UnidadeMedidaId)
            .HasColumnName("unidade_medida_id")
            .IsRequired();

        builder.Property(si => si.Quantidade)
            .HasColumnName("quantidade")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(si => si.CustoUnitario)
            .HasColumnName("custo_unitario")
            .HasPrecision(16, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(si => si.CustoTotal)
            .HasColumnName("custo_total")
            .HasPrecision(16, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(si => si.Lote)
            .HasColumnName("lote")
            .HasMaxLength(100);

        builder.Property(si => si.DataValidade)
            .HasColumnName("data_validade")
            .HasColumnType("timestamptz");

        builder.Property(si => si.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(255);

        builder.Property(si => si.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(si => si.SaidaEstoque)
            .WithMany(s => s.Itens)
            .HasForeignKey(si => si.SaidaEstoqueId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_saida_estoque_item_saida_estoque");

        builder.HasOne(si => si.Produto)
            .WithMany()
            .HasForeignKey(si => si.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_saida_estoque_item_produto");

        builder.HasOne(si => si.UnidadeMedida)
            .WithMany()
            .HasForeignKey(si => si.UnidadeMedidaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_saida_estoque_item_unidade_medida");

        // Índices
        builder.HasIndex(si => si.SaidaEstoqueId)
            .HasDatabaseName("ix_saida_estoque_item_saida_estoque_id");

        builder.HasIndex(si => si.ProdutoId)
            .HasDatabaseName("ix_saida_estoque_item_produto_id");

        builder.HasIndex(si => si.UnidadeMedidaId)
            .HasDatabaseName("ix_saida_estoque_item_unidade_medida_id");

        builder.HasIndex(si => si.Lote)
            .HasDatabaseName("ix_saida_estoque_item_lote");

        builder.HasIndex(si => si.DataValidade)
            .HasDatabaseName("ix_saida_estoque_item_data_validade");
    }
}
