using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("auditoria");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(a => a.UsuarioId)
            .HasColumnName("usuario_id");

        builder.Property(a => a.RegistroId)
            .HasColumnName("registro_id");

        builder.Property(a => a.Tabela)
            .HasColumnName("tabela")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Tipo)
            .HasColumnName("tipo")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.DadosAnteriores)
            .HasColumnName("dados_anteriores")
            .HasColumnType("jsonb");

        builder.Property(a => a.DadosNovos)
            .HasColumnName("dados_novos")
            .HasColumnType("jsonb");

        builder.Property(a => a.Ip)
            .HasColumnName("ip")
            .HasMaxLength(45);

        builder.Property(a => a.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);

        builder.Property(a => a.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Relacionamento
        builder.HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_auditoria_usuario");

        // Índices
        builder.HasIndex(a => a.UsuarioId)
            .HasDatabaseName("ix_auditoria_usuario_id");

        builder.HasIndex(a => a.Tabela)
            .HasDatabaseName("ix_auditoria_tabela");

        builder.HasIndex(a => a.RegistroId)
            .HasDatabaseName("ix_auditoria_registro_id");

        builder.HasIndex(a => a.Tipo)
            .HasDatabaseName("ix_auditoria_tipo");

        builder.HasIndex(a => a.CriadoEm)
            .HasDatabaseName("ix_auditoria_criado_em");

        builder.HasIndex(a => new { a.Tabela, a.RegistroId })
            .HasDatabaseName("ix_auditoria_tabela_registro");
    }
}
