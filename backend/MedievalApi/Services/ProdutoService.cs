using MedievalApi.Data;
using MedievalApi.DTOs.Produto;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class ProdutoService(AppDbContext appDbContext) : IProdutoService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<ProdutoResponse> CreateAsync(ProdutoCreateRequest request)
    {
        await ValidarGrupoProdutoAsync(request.GrupoProdutoId);
        await ValidarUnidadeMedidaAsync(request.UnidadeMedidaId);

        var codigoNormalizado = NormalizeCodigo(request.Codigo);

        if (!string.IsNullOrWhiteSpace(codigoNormalizado) &&
            await context.Produtos.AnyAsync(p => p.Codigo == codigoNormalizado))
        {
            throw new ConflictException($"Já existe um produto com o código '{codigoNormalizado}'.");
        }

        var produto = new Produto
        {
            GrupoProdutoId = request.GrupoProdutoId,
            UnidadeMedidaId = request.UnidadeMedidaId,
            Nome = request.Nome,
            Codigo = request.Codigo,
            Descricao = request.Descricao,
            EstoqueMinimo = request.EstoqueMinimo,
            EstoqueMaximo = request.EstoqueMaximo,
            ValidadeDias = request.ValidadeDias
        };

        context.Produtos.Add(produto);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException($"Já existe um produto com o código '{codigoNormalizado}'.");
        }

        await CarregarNavegacoesAsync(produto);
        return ToResponse(produto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var produto = await context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return false;

        context.Produtos.Remove(produto);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException(
                "Não é possível excluir o produto pois existem registros vinculados (movimentações, pedidos, etc.).");
        }

        return true;
    }

    public async Task<IEnumerable<ProdutoResponse>> GetAllAsync()
    {
        var produtos = await context.Produtos
            .AsNoTracking()
            .Include(p => p.GrupoProduto)
            .Include(p => p.UnidadeMedida)
            .OrderBy(p => p.Nome)
            .ToListAsync();

        return produtos.Select(ToResponse);
    }

    public async Task<ProdutoResponse?> GetByIdAsync(int id)
    {
        var produto = await context.Produtos
            .AsNoTracking()
            .Include(p => p.GrupoProduto)
            .Include(p => p.UnidadeMedida)
            .FirstOrDefaultAsync(p => p.Id == id);

        return produto == null ? null : ToResponse(produto);
    }

    public async Task<ProdutoResponse?> UpdateAsync(int id, ProdutoUpdateRequest request)
    {
        var produto = await context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return null;

        await ValidarGrupoProdutoAsync(request.GrupoProdutoId);
        await ValidarUnidadeMedidaAsync(request.UnidadeMedidaId);

        var codigoNormalizado = NormalizeCodigo(request.Codigo);

        if (!string.IsNullOrWhiteSpace(codigoNormalizado) &&
            await context.Produtos.AnyAsync(p => p.Codigo == codigoNormalizado && p.Id != id))
        {
            throw new ConflictException($"Já existe outro produto com o código '{codigoNormalizado}'.");
        }

        produto.GrupoProdutoId = request.GrupoProdutoId;
        produto.UnidadeMedidaId = request.UnidadeMedidaId;
        produto.Nome = request.Nome;
        produto.Codigo = request.Codigo;
        produto.Descricao = request.Descricao;
        produto.EstoqueMinimo = request.EstoqueMinimo;
        produto.EstoqueMaximo = request.EstoqueMaximo;
        produto.ValidadeDias = request.ValidadeDias;
        produto.Ativo = request.Ativo;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException($"Já existe outro produto com o código '{codigoNormalizado}'.");
        }

        await CarregarNavegacoesAsync(produto);
        return ToResponse(produto);
    }

    private async Task ValidarGrupoProdutoAsync(int grupoProdutoId)
    {
        if (!await context.GruposProduto.AnyAsync(g => g.Id == grupoProdutoId))
            throw new NotFoundException("Grupo de produto", grupoProdutoId);
    }

    private async Task ValidarUnidadeMedidaAsync(int unidadeMedidaId)
    {
        if (!await context.UnidadesMedida.AnyAsync(u => u.Id == unidadeMedidaId))
            throw new NotFoundException("Unidade de medida", unidadeMedidaId);
    }

    private async Task CarregarNavegacoesAsync(Produto produto)
    {
        await context.Entry(produto).Reference(p => p.GrupoProduto).LoadAsync();
        await context.Entry(produto).Reference(p => p.UnidadeMedida).LoadAsync();
    }

    private static string? NormalizeCodigo(string? codigo) =>
        codigo?.Trim().ToUpperInvariant();

    private static ProdutoResponse ToResponse(Produto p) => new(
        p.Id,
        p.GrupoProdutoId,
        p.GrupoProduto?.Nome ?? string.Empty,
        p.UnidadeMedidaId,
        p.UnidadeMedida?.Sigla ?? string.Empty,
        p.Codigo,
        p.Nome,
        p.Descricao,
        p.EstoqueMinimo,
        p.EstoqueMaximo,
        p.CustoMedioEstoque,
        p.UltimoCusto,
        p.ValidadeDias,
        p.Ativo,
        p.CriadoEm,
        p.AtualizadoEm
    );

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("duplicate key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23505", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
