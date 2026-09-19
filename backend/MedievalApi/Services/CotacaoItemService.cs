using MedievalApi.Data;
using MedievalApi.DTOs.CotacaoItem;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Models.Enums;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class CotacaoItemService(AppDbContext appDbContext) : ICotacaoItemService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<CotacaoItemResponse> CreateAsync(CotacaoItemCreateRequest request)
    {
        await ValidateCotacaoAsync(request.CotacaoId);
        await ValidateProdutoAsync(request.ProdutoId);
        await ValidateUnidadeMedidaAsync(request.UnidadeMedidaId);

        var item = new CotacaoItem
        {
            CotacaoId = request.CotacaoId,
            ProdutoId = request.ProdutoId,
            UnidadeMedidaId = request.UnidadeMedidaId,
            Quantidade = request.Quantidade,
            Status = request.Status,
            Observacao = request.Observacao
        };

        context.Set<CotacaoItem>().Add(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar item da cotação.");
        }

        return ToResponse(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await context.Set<CotacaoItem>()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null) return false;

        if (item.Status != StatusCotacaoItem.Pendente)
            throw new BusinessException(
                "Só é possível excluir itens de cotações pendentes."
            );

        context.Set<CotacaoItem>().Remove(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException(
                "Não é possível excluir o item da cotação pois existem registros vinculados."
            );
        }

        return true;
    }

    public async Task<IEnumerable<CotacaoItemResponse>> GetAllAsync()
    {
        var itens = await context.Set<CotacaoItem>()
            .AsNoTracking()
            .Include(i => i.Produto)
            .Include(i => i.UnidadeMedida)
            .OrderByDescending(i => i.CriadoEm)
            .ToListAsync();

        return itens.Select(ToResponse);
    }

    public async Task<CotacaoItemResponse?> GetByIdAsync(int id)
    {
        var item = await context.Set<CotacaoItem>()
            .AsNoTracking()
            .Include(i => i.Produto)
            .Include(i => i.UnidadeMedida)
            .FirstOrDefaultAsync(i => i.Id == id);

        return item == null ? null : ToResponse(item);
    }

    public async Task<IEnumerable<CotacaoItemResponse>> GetByCotacaoIdAsync(int cotacaoId)
    {
        await ValidateCotacaoAsync(cotacaoId);

        var itens = await context.Set<CotacaoItem>()
            .AsNoTracking()
            .Include(i => i.Produto)
            .Include(i => i.UnidadeMedida)
            .Where(i => i.CotacaoId == cotacaoId)
            .OrderBy(i => i.Id)
            .ToListAsync();

        return itens.Select(ToResponse);
    }

    public async Task<CotacaoItemResponse?> UpdateAsync(
        int id,
        CotacaoItemUpdateRequest request)
    {
        var item = await context.Set<CotacaoItem>()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null) return null;

        if (item.Status != StatusCotacaoItem.Pendente)
            throw new BusinessException(
                "Só é possível atualizar itens de cotação pendentes."
            );

        if (item.CotacaoId != request.CotacaoId)
            await ValidateCotacaoAsync(request.CotacaoId);

        if (item.ProdutoId != request.ProdutoId)
            await ValidateProdutoAsync(request.ProdutoId);

        if (item.UnidadeMedidaId != request.UnidadeMedidaId)
            await ValidateUnidadeMedidaAsync(request.UnidadeMedidaId);

        item.CotacaoId = request.CotacaoId;
        item.ProdutoId = request.ProdutoId;
        item.UnidadeMedidaId = request.UnidadeMedidaId;
        item.Quantidade = request.Quantidade;
        item.Status = request.Status;
        item.Observacao = request.Observacao;
        item.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException(
                "Referência inválida ao atualizar item da cotação."
            );
        }

        return ToResponse(item);
    }

    private async Task ValidateCotacaoAsync(int cotacaoId)
    {
        if (!await context.Set<Cotacao>()
            .AnyAsync(c => c.Id == cotacaoId))
        {
            throw new NotFoundException("Cotacao", cotacaoId);
        }
    }

    private async Task ValidateProdutoAsync(int produtoId)
    {
        if (!await context.Set<Produto>()
            .AnyAsync(p => p.Id == produtoId))
        {
            throw new NotFoundException("Produto", produtoId);
        }
    }

    private async Task ValidateUnidadeMedidaAsync(int unidadeMedidaId)
    {
        if (!await context.Set<UnidadeMedida>()
            .AnyAsync(u => u.Id == unidadeMedidaId))
        {
            throw new NotFoundException("UnidadeMedida", unidadeMedidaId);
        }
    }

    private static CotacaoItemResponse ToResponse(CotacaoItem item) => new(
        item.Id,
        item.CotacaoId,
        item.ProdutoId,
        item.Produto?.Nome ?? string.Empty,
        item.UnidadeMedidaId,
        item.UnidadeMedida?.Nome ?? string.Empty,
        item.Quantidade,
        item.Status,
        item.Observacao,
        item.CriadoEm,
        item.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;

        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
