using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedievalApi.Models;
using MedievalApi.Models.Enums;

namespace MedievalApi.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(u => u.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.SenhaHash)
            .HasColumnName("senha_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.GrupoUsuario)
            .HasColumnName("grupo_usuario")
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(GrupoUsuario.Consulta)
            .IsRequired();

        builder.Property(u => u.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(StatusUsuario.Ativo)
            .IsRequired();

        builder.Property(u => u.UltimoLogin)
            .HasColumnName("ultimo_login");

        builder.Property(u => u.CriadoEm)
            .HasColumnName("criado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(u => u.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Índices
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ix_usuario_email");

        builder.HasIndex(u => u.GrupoUsuario)
            .HasDatabaseName("ix_usuario_grupo_usuario");

        builder.HasIndex(u => u.Status)
            .HasDatabaseName("ix_usuario_status");
    }
}
