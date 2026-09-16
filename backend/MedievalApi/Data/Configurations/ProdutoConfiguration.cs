using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedievalApi.Models;

namespace MedievalApi.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produto");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(p => p.CategoriaId)
            .HasColumnName("categoria_id")
            .HasColumnType("uuid");

        builder.HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_produto_categoria");

        builder.Property(p => p.UnidadeMedidaId)
            .HasColumnName("unidade_medida_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasOne(p => p.UnidadeMedida)
            .WithMany(u => u.Produtos)
            .HasForeignKey(p => p.UnidadeMedidaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_produto_unidade_medida");

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
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(p => p.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Índices
        builder.HasIndex(p => p.Codigo)
            .IsUnique()
            .HasDatabaseName("ix_produto_codigo")
            .HasFilter("codigo IS NOT NULL");

        builder.HasIndex(p => p.Nome)
            .HasDatabaseName("ix_produto_nome");

        builder.HasIndex(p => p.CategoriaId)
            .HasDatabaseName("ix_produto_categoria_id");

        builder.HasIndex(p => p.UnidadeMedidaId)
            .HasDatabaseName("ix_produto_unidade_medida_id");

        builder.HasIndex(p => p.Ativo)
            .HasDatabaseName("ix_produto_ativo");

        builder.HasIndex(p => new { p.CategoriaId, p.Ativo })
            .HasDatabaseName("ix_produto_categoria_ativo");
    }
}
