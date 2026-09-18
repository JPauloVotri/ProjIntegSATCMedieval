using MedievalApi.Models;
using MedievalApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class CotacaoItemConfiguration : IEntityTypeConfiguration<CotacaoItem>
{
    public void Configure(EntityTypeBuilder<CotacaoItem> builder)
    {
        builder.ToTable("cotacao_item");

        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(ci => ci.CotacaoId)
            .HasColumnName("cotacao_id")
            .IsRequired();

        builder.Property(ci => ci.ProdutoId)
            .HasColumnName("produto_id")
            .IsRequired();

        builder.Property(ci => ci.UnidadeMedidaId)
            .HasColumnName("unidade_medida_id")
            .IsRequired();

        builder.Property(ci => ci.Quantidade)
            .HasColumnName("quantidade")
            .HasPrecision(14, 3)
            .IsRequired();

        builder.Property(ci => ci.CotacaoItemFornecedorEscolhidoId)
            .HasColumnName("cotacao_item_fornecedor_escolhido_id");

        builder.Property(ci => ci.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(StatusCotacaoItem.Pendente)
            .IsRequired();

        builder.Property(ci => ci.Observacao)
            .HasColumnName("observacao")
            .HasMaxLength(255);

        builder.Property(ci => ci.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(ci => ci.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(ci => ci.Cotacao)
            .WithMany(c => c.Itens)
            .HasForeignKey(ci => ci.CotacaoId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_cotacao_item_cotacao");

        builder.HasOne(ci => ci.Produto)
            .WithMany()
            .HasForeignKey(ci => ci.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_cotacao_item_produto");

        builder.HasOne(ci => ci.UnidadeMedida)
            .WithMany()
            .HasForeignKey(ci => ci.UnidadeMedidaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_cotacao_item_unidade_medida");

        // TODO: Verificar se vai ficar assim mesmo
        builder.HasOne(ci => ci.CotacaoItemFornecedorEscolhido)
            .WithMany()
            .HasForeignKey(ci => ci.CotacaoItemFornecedorEscolhidoId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_cotacao_item_fornecedor_escolhido");

        // Índices
        builder.HasIndex(ci => ci.CotacaoId)
            .HasDatabaseName("ix_cotacao_item_cotacao_id");

        builder.HasIndex(ci => ci.ProdutoId)
            .HasDatabaseName("ix_cotacao_item_produto_id");

        builder.HasIndex(ci => ci.UnidadeMedidaId)
            .HasDatabaseName("ix_cotacao_item_unidade_medida_id");

        builder.HasIndex(ci => ci.CotacaoItemFornecedorEscolhidoId)
            .HasDatabaseName("ix_cotacao_item_fornecedor_escolhido_id");

        builder.HasIndex(ci => ci.Status)
            .HasDatabaseName("ix_cotacao_item_status");
    }
}
