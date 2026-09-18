using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class CotacaoItemFornecedorConfiguration : IEntityTypeConfiguration<CotacaoItemFornecedor>
{
    public void Configure(EntityTypeBuilder<CotacaoItemFornecedor> builder)
    {
        builder.ToTable("cotacao_item_fornecedor");

        builder.HasKey(cif => cif.Id);
        builder.Property(cif => cif.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(cif => cif.CotacaoItemId)
            .HasColumnName("cotacao_item_id")
            .IsRequired();

        builder.Property(cif => cif.FornecedorId)
            .HasColumnName("fornecedor_id")
            .IsRequired();

        builder.Property(cif => cif.ValorUnitario)
            .HasColumnName("valor_unitario")
            .HasPrecision(16, 4)
            .IsRequired();

        builder.Property(cif => cif.PrazoEntregaDias)
            .HasColumnName("prazo_entrega_dias");

        builder.Property(cif => cif.CondicaoPagamento)
            .HasColumnName("condicao_pagamento")
            .HasMaxLength(150);

        builder.Property(cif => cif.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(255);

        builder.Property(cif => cif.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(cif => cif.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // TODO: Verificar se vai ficar assim mesmo
        builder.HasOne(cif => cif.CotacaoItem)
            .WithMany()
            .HasForeignKey(cif => cif.CotacaoItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_cotacao_item_fornecedor_cotacao_item");

        builder.HasOne(cif => cif.Fornecedor)
            .WithMany()
            .HasForeignKey(cif => cif.FornecedorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_cotacao_item_fornecedor_fornecedor");

        // Índices
        builder.HasIndex(cif => new { cif.CotacaoItemId, cif.FornecedorId })
            .IsUnique()
            .HasDatabaseName("ix_cotacao_item_fornecedor_unique");

        builder.HasIndex(cif => cif.FornecedorId)
            .HasDatabaseName("ix_cotacao_item_fornecedor_fornecedor_id");
    }
}
