using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class PedidoCompraItemConfiguration : IEntityTypeConfiguration<PedidoCompraItem>
{
    public void Configure(EntityTypeBuilder<PedidoCompraItem> builder)
    {
        builder.ToTable("pedido_compra_item");

        builder.HasKey(pci => pci.Id);
        builder.Property(pci => pci.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(pci => pci.PedidoCompraId)
            .HasColumnName("pedido_compra_id")
            .IsRequired();

        builder.Property(pci => pci.ProdutoId)
            .HasColumnName("produto_id")
            .IsRequired();

        builder.Property(pci => pci.UnidadeMedidaId)
            .HasColumnName("unidade_medida_id")
            .IsRequired();

        builder.Property(pci => pci.CotacaoItemId)
            .HasColumnName("cotacao_item_id");

        builder.Property(pci => pci.QuantidadeSolicitada)
            .HasColumnName("quantidade_solicitada")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(pci => pci.QuantidadeRecebida)
            .HasColumnName("quantidade_recebida")
            .HasPrecision(14, 3)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(pci => pci.ValorUnitario)
            .HasColumnName("valor_unitario")
            .HasPrecision(16, 4)
            .IsRequired();

        builder.Property(pci => pci.Total)
            .HasColumnName("total")
            .HasPrecision(16, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(pci => pci.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(255);

        builder.Property(pci => pci.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(pci => pci.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(pci => pci.PedidoCompra)
            .WithMany(pc => pc.Itens)
            .HasForeignKey(pci => pci.PedidoCompraId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_pedido_compra_item_pedido_compra");

        builder.HasOne(pci => pci.Produto)
            .WithMany()
            .HasForeignKey(pci => pci.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_pedido_compra_item_produto");

        builder.HasOne(pci => pci.UnidadeMedida)
            .WithMany()
            .HasForeignKey(pci => pci.UnidadeMedidaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_pedido_compra_item_unidade_medida");

        builder.HasOne(pci => pci.CotacaoItem)
            .WithMany()
            .HasForeignKey(pci => pci.CotacaoItemId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_pedido_compra_item_cotacao_item");

        // Índices
        builder.HasIndex(pci => pci.PedidoCompraId)
            .HasDatabaseName("ix_pedido_compra_item_pedido_compra_id");

        builder.HasIndex(pci => pci.ProdutoId)
            .HasDatabaseName("ix_pedido_compra_item_produto_id");

        builder.HasIndex(pci => pci.UnidadeMedidaId)
            .HasDatabaseName("ix_pedido_compra_item_unidade_medida_id");

        builder.HasIndex(pci => pci.CotacaoItemId)
            .HasDatabaseName("ix_pedido_compra_item_cotacao_item_id");
    }
}
