using MedievalApi.Data;
using MedievalApi.DTOs.Estoque;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class EstoqueService(AppDbContext appDbContext) : IEstoqueService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<EstoqueResponse> CreateAsync(EstoqueCreateRequest request)
    {
        await ValidateProdutoAsync(request.ProdutoId);

        var estoque = new Estoque
        {
            ProdutoId = request.ProdutoId,
            QuantidadeDisponivel = request.QuantidadeDisponivel,
            ValorTotal = request.ValorTotal,
            UltimaEntrada = request.UltimaEntrada,
            UltimaSaida = request.UltimaSaida
        };

        context.Set<Estoque>().Add(estoque);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar estoque.");
        }

        await context.Entry(estoque).Reference(e => e.Produto).LoadAsync();

        return ToResponse(estoque);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estoque = await context.Set<Estoque>().FirstOrDefaultAsync(e => e.Id == id);
        if (estoque == null) return false;

        context.Set<Estoque>().Remove(estoque);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir o estoque pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<EstoqueResponse>> GetAllAsync()
    {
        var estoques = await context.Set<Estoque>()
            .AsNoTracking()
            .Include(e => e.Produto)
            .OrderBy(e => e.ProdutoId)
            .ToListAsync();

        return estoques.Select(ToResponse);
    }

    public async Task<EstoqueResponse?> GetByIdAsync(int id)
    {
        var estoque = await context.Set<Estoque>()
            .AsNoTracking()
            .Include(e => e.Produto)
            .FirstOrDefaultAsync(e => e.Id == id);

        return estoque == null ? null : ToResponse(estoque);
    }

    public async Task<EstoqueResponse?> UpdateAsync(int id, EstoqueUpdateRequest request)
    {
        var estoque = await context.Set<Estoque>().FirstOrDefaultAsync(e => e.Id == id);
        if (estoque == null) return null;

        if (estoque.ProdutoId != request.ProdutoId)
            await ValidateProdutoAsync(request.ProdutoId);

        estoque.ProdutoId = request.ProdutoId;
        estoque.QuantidadeDisponivel = request.QuantidadeDisponivel;
        estoque.ValorTotal = request.ValorTotal;
        estoque.UltimaEntrada = request.UltimaEntrada;
        estoque.UltimaSaida = request.UltimaSaida;
        estoque.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar estoque.");
        }

        await context.Entry(estoque).Reference(e => e.Produto).LoadAsync();

        return ToResponse(estoque);
    }

    private async Task ValidateProdutoAsync(int produtoId)
    {
        if (!await context.Produtos.AnyAsync(p => p.Id == produtoId))
            throw new NotFoundException("Produto", produtoId);
    }

    private static EstoqueResponse ToResponse(Estoque e) => new(
        e.Id,
        e.ProdutoId,
        e.Produto?.Nome ?? string.Empty,
        e.QuantidadeDisponivel,
        e.ValorTotal,
        e.UltimaEntrada,
        e.UltimaSaida,
        e.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
