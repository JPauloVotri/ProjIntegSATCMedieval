using System.Reflection;
using MedievalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<GrupoProduto> GruposProduto => Set<GrupoProduto>();
    public DbSet<UnidadeMedida> UnidadesMedida => Set<UnidadeMedida>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<Cotacao> Cotacoes => Set<Cotacao>();
    public DbSet<CotacaoItem> CotacaoItens => Set<CotacaoItem>();
    public DbSet<CotacaoItemFornecedor> CotacaoItemFornecedores => Set<CotacaoItemFornecedor>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<PedidoCompra> PedidoCompras => Set<PedidoCompra>();
    public DbSet<PedidoCompraItem> PedidoCompraItens => Set<PedidoCompraItem>();
    public DbSet<SaidaEstoque> SaidaEstoques => Set<SaidaEstoque>();
    public DbSet<SaidaEstoqueItem> SaidaEstoqueItens => Set<SaidaEstoqueItem>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    
    private void UpdateTimestamps()
    {
    DateTime now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                var criadoEm = entry.Metadata.FindProperty("CriadoEm");
                var atualizadoEm = entry.Metadata.FindProperty("AtualizadoEm");

                if (criadoEm != null)
                    entry.Property("CriadoEm").CurrentValue = now;

                if (atualizadoEm != null)
                    entry.Property("AtualizadoEm").CurrentValue = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                var atualizadoEm = entry.Metadata.FindProperty("AtualizadoEm");

                if (atualizadoEm != null)
                    entry.Property("AtualizadoEm").CurrentValue = now;
            }
        }
    }

}
