using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class MovimentacaoEstoqueConfiguration : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.ToTable("movimentacao_estoque");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(m => m.EstoqueId)
            .HasColumnName("estoque_id")
            .IsRequired();

        builder.Property(m => m.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(m => m.ReferenciaId)
            .HasColumnName("referencia_id");

        builder.Property(m => m.ReferenciaTipo)
            .HasColumnName("referencia_tipo")
            .HasMaxLength(50);

        builder.Property(m => m.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.Origem)
            .HasColumnName("origem")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(m => m.Quantidade)
            .HasColumnName("quantidade")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(m => m.QuantidadeAnterior)
            .HasColumnName("quantidade_anterior")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(m => m.QuantidadePosterior)
            .HasColumnName("quantidade_posterior")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(m => m.CustoUnitario)
            .HasColumnName("custo_unitario")
            .HasPrecision(16, 4);

        builder.Property(m => m.ValorTotal)
            .HasColumnName("valor_total")
            .HasPrecision(16, 4);

        builder.Property(m => m.Lote)
            .HasColumnName("lote")
            .HasMaxLength(100);

        builder.Property(m => m.DataValidade)
            .HasColumnName("data_validade")
            .HasColumnType("timestamptz");

        builder.Property(m => m.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(255);

        builder.Property(m => m.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(m => m.Estoque)
            .WithMany()
            .HasForeignKey(m => m.EstoqueId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_movimentacao_estoque_estoque");

        builder.HasOne(m => m.Usuario)
            .WithMany()
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_movimentacao_estoque_usuario");

        // Índices
        builder.HasIndex(m => m.EstoqueId)
            .HasDatabaseName("ix_movimentacao_estoque_estoque_id");

        builder.HasIndex(m => m.UsuarioId)
            .HasDatabaseName("ix_movimentacao_estoque_usuario_id");

        builder.HasIndex(m => m.Tipo)
            .HasDatabaseName("ix_movimentacao_estoque_tipo");

        builder.HasIndex(m => m.Origem)
            .HasDatabaseName("ix_movimentacao_estoque_origem");

        builder.HasIndex(m => m.CriadoEm)
            .HasDatabaseName("ix_movimentacao_estoque_criado_em");

        builder.HasIndex(m => new { m.ReferenciaTipo, m.ReferenciaId })
            .HasDatabaseName("ix_movimentacao_estoque_referencia");
    }
}
