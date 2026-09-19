using MedievalApi.Models;
using MedievalApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class PedidoCompraConfiguration : IEntityTypeConfiguration<PedidoCompra>
{
    public void Configure(EntityTypeBuilder<PedidoCompra> builder)
    {
        builder.ToTable("pedido_compra");

        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(pc => pc.FornecedorId)
            .HasColumnName("fornecedor_id")
            .IsRequired();

        builder.Property(pc => pc.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(pc => pc.CotacaoId)
            .HasColumnName("cotacao_id");

        builder.Property(pc => pc.DataPedido)
            .HasColumnName("data_pedido")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(pc => pc.DataPrevistaEntrega)
            .HasColumnName("data_prevista_entrega")
            .HasColumnType("timestamptz");

        builder.Property(pc => pc.DataRecebimento)
            .HasColumnName("data_recebimento")
            .HasColumnType("timestamptz");

        builder.Property(pc => pc.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(30)
            .HasDefaultValue(StatusPedidoCompra.Rascunho)
            .IsRequired();

        builder.Property(pc => pc.Total)
            .HasColumnName("total")
            .HasPrecision(16, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(pc => pc.Observacao)
            .HasColumnName("observacao");

        builder.Property(pc => pc.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(pc => pc.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(pc => pc.Fornecedor)
            .WithMany()
            .HasForeignKey(pc => pc.FornecedorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_pedido_compra_fornecedor");

        builder.HasOne(pc => pc.Usuario)
            .WithMany()
            .HasForeignKey(pc => pc.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_pedido_compra_usuario");

        builder.HasOne(pc => pc.Cotacao)
            .WithMany()
            .HasForeignKey(pc => pc.CotacaoId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_pedido_compra_cotacao");

        // Índices
        builder.HasIndex(pc => pc.FornecedorId)
            .HasDatabaseName("ix_pedido_compra_fornecedor_id");

        builder.HasIndex(pc => pc.UsuarioId)
            .HasDatabaseName("ix_pedido_compra_usuario_id");

        builder.HasIndex(pc => pc.CotacaoId)
            .HasDatabaseName("ix_pedido_compra_cotacao_id");

        builder.HasIndex(pc => pc.Status)
            .HasDatabaseName("ix_pedido_compra_status");

        builder.HasIndex(pc => pc.DataPedido)
            .HasDatabaseName("ix_pedido_compra_data_pedido");
    }
}
