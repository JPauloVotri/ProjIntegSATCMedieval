using MedievalApi.Models;
using MedievalApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class SaidaEstoqueConfiguration : IEntityTypeConfiguration<SaidaEstoque>
{
    public void Configure(EntityTypeBuilder<SaidaEstoque> builder)
    {
        builder.ToTable("saida_estoque");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(s => s.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(s => s.DataSaida)
            .HasColumnName("data_saida")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(s => s.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(StatusSaidaEstoque.Rascunho)
            .IsRequired();

        builder.Property(s => s.Motivo)
            .HasColumnName("motivo")
            .HasMaxLength(255);

        builder.Property(s => s.Observacao)
            .HasColumnName("observacao");

        builder.Property(s => s.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(s => s.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamentos
        builder.HasOne(s => s.Usuario)
            .WithMany()
            .HasForeignKey(s => s.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_saida_estoque_usuario");

        // Índices
        builder.HasIndex(s => s.UsuarioId)
            .HasDatabaseName("ix_saida_estoque_usuario_id");

        builder.HasIndex(s => s.DataSaida)
            .HasDatabaseName("ix_saida_estoque_data_saida");

        builder.HasIndex(s => s.Tipo)
            .HasDatabaseName("ix_saida_estoque_tipo");

        builder.HasIndex(s => s.Status)
            .HasDatabaseName("ix_saida_estoque_status");
    }
}
