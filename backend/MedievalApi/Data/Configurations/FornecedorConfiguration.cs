using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedievalApi.Data.Configurations;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("fornecedor");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .HasColumnName("id")
            .UseIdentityAlwaysColumn();

        builder.Property(f => f.RazaoSocial)
            .HasColumnName("razao_social")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(f => f.NomeFantasia)
            .HasColumnName("nome_fantasia")
            .HasMaxLength(200);

        builder.Property(f => f.Documento)
            .HasColumnName("documento")
            .HasMaxLength(30);

        builder.Property(f => f.InscricaoEstadual)
            .HasColumnName("inscricao_estadual")
            .HasMaxLength(30);

        builder.Property(f => f.Email)
            .HasColumnName("email")
            .HasMaxLength(150);

        builder.Property(f => f.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(30);

        builder.Property(f => f.NomeContato)
            .HasColumnName("nome_contato")
            .HasMaxLength(150);

        builder.Property(f => f.Cep).HasColumnName("cep").HasMaxLength(10);
        builder.Property(f => f.Logradouro).HasColumnName("logradouro").HasMaxLength(200);
        builder.Property(f => f.Numero).HasColumnName("numero").HasMaxLength(20);
        builder.Property(f => f.Complemento).HasColumnName("complemento").HasMaxLength(100);
        builder.Property(f => f.Bairro).HasColumnName("bairro").HasMaxLength(100);
        builder.Property(f => f.Cidade).HasColumnName("cidade").HasMaxLength(100);
        builder.Property(f => f.Estado).HasColumnName("estado").HasMaxLength(2);
        builder.Property(f => f.DiasEntrega).HasColumnName("dias_entrega").HasMaxLength(50);
        builder.Property(f => f.CondicaoPagamento).HasColumnName("condicao_pagamento").HasMaxLength(150);

        builder.Property(f => f.Ativo)
            .HasColumnName("ativo")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(f => f.CriadoEm)
            .HasColumnName("criado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(f => f.AtualizadoEm)
            .HasColumnName("atualizado_em")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasIndex(f => f.Documento)
            .HasDatabaseName("ix_fornecedor_documento");

        builder.HasIndex(f => f.RazaoSocial)
            .HasDatabaseName("ix_fornecedor_razao_social");

        builder.HasIndex(f => f.NomeFantasia)
            .HasDatabaseName("ix_fornecedor_nome_fantasia");

        builder.HasIndex(f => f.Ativo)
            .HasDatabaseName("ix_fornecedor_ativo");
    }
}
