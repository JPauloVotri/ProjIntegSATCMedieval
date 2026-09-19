using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produto");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(p => p.GrupoProdutoId)
            .HasColumnName("grupo_produto_id")
            .IsRequired();

        builder.Property(p => p.UnidadeMedidaId)
            .HasColumnName("unidade_medida_id")
            .IsRequired();

        builder.Property(p => p.Codigo)
            .HasColumnName("codigo")
            .HasMaxLength(50);

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(255);

        builder.Property(p => p.EstoqueMinimo)
            .HasColumnName("estoque_minimo")
            .HasPrecision(14, 3)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(p => p.EstoqueMaximo)
            .HasColumnName("estoque_maximo")
            .HasPrecision(14, 3);

        builder.Property(p => p.CustoMedioEstoque)
            .HasColumnName("custo_medio_estoque")
            .HasPrecision(14, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(p => p.UltimoCusto)
            .HasColumnName("ultimo_custo")
            .HasPrecision(14, 4)
            .HasDefaultValue(0m)
            .IsRequired();

        builder.Property(p => p.ValidadeDias)
            .HasColumnName("validade_dias");

        builder.Property(p => p.Ativo)
            .HasColumnName("ativo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(p => p.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasOne(p => p.GrupoProduto)
            .WithMany(g => g.Produtos)
            .HasForeignKey(p => p.GrupoProdutoId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_produto_grupo_produto");

        builder.HasOne(p => p.UnidadeMedida)
            .WithMany(u => u.Produtos)
            .HasForeignKey(p => p.UnidadeMedidaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_produto_unidade_medida");

        builder.HasOne(p => p.Estoque)
            .WithOne(e => e.Produto)
            .HasForeignKey<Estoque>(e => e.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_estoque_produto");

        // Índices
        builder.HasIndex(p => p.Codigo)
            .IsUnique()
            .HasDatabaseName("ix_produto_codigo")
            .HasFilter("codigo IS NOT NULL");

        builder.HasIndex(p => p.Nome)
            .HasDatabaseName("ix_produto_nome");

        builder.HasIndex(p => p.GrupoProdutoId)
            .HasDatabaseName("ix_produto_grupo_produto_id");

        builder.HasIndex(p => p.UnidadeMedidaId)
            .HasDatabaseName("ix_produto_unidade_medida_id");

        builder.HasIndex(p => p.Ativo)
            .HasDatabaseName("ix_produto_ativo");

        builder.HasIndex(p => new { p.GrupoProdutoId, p.Ativo })
            .HasDatabaseName("ix_produto_grupo_produto_ativo");
    }
}
