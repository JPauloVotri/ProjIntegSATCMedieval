using MedievalApi.Models;
using MedievalApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class CotacaoConfiguration : IEntityTypeConfiguration<Cotacao>
{
    public void Configure(EntityTypeBuilder<Cotacao> builder)
    {
        builder.ToTable("cotacao");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(c => c.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired();

        builder.Property(c => c.DataSolicitacao)
            .HasColumnName("data_solicitacao")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(c => c.DataLimiteResposta)
            .HasColumnName("data_limite_resposta")
            .HasColumnType("timestamptz");

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(StatusCotacao.Rascunho)
            .IsRequired();

        builder.Property(c => c.Observacao)
            .HasColumnName("observacao");

        builder.Property(c => c.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(c => c.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_cotacao_usuario");

        // Índices
        builder.HasIndex(c => c.UsuarioId)
            .HasDatabaseName("ix_cotacao_usuario_id");

        builder.HasIndex(c => c.Status)
            .HasDatabaseName("ix_cotacao_status");

        builder.HasIndex(c => c.DataSolicitacao)
            .HasDatabaseName("ix_cotacao_data_solicitacao");

        builder.HasIndex(c => c.DataLimiteResposta)
            .HasDatabaseName("ix_cotacao_data_limite_resposta");
    }
}
