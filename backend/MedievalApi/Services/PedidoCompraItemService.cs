using MedievalApi.Data;
using MedievalApi.DTOs.PedidoCompraItem;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class PedidoCompraItemService(AppDbContext appDbContext) : IPedidoCompraItemService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<PedidoCompraItemResponse> CreateAsync(PedidoCompraItemCreateRequest request)
    {
        await ValidatePedidoAsync(request.PedidoCompraId);
        await ValidateProdutoAsync(request.ProdutoId);
        await ValidateUnidadeMedidaAsync(request.UnidadeMedidaId);

        var item = new PedidoCompraItem
        {
            PedidoCompraId = request.PedidoCompraId,
            ProdutoId = request.ProdutoId,
            UnidadeMedidaId = request.UnidadeMedidaId,
            CotacaoItemId = request.CotacaoItemId,
            QuantidadeSolicitada = request.QuantidadeSolicitada,
            QuantidadeRecebida = request.QuantidadeRecebida,
            ValorUnitario = request.ValorUnitario,
            Total = request.Total,
            Observacao = request.Observacao
        };

        context.Set<PedidoCompraItem>().Add(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar item do pedido de compra.");
        }

        await context.Entry(item).Reference(i => i.Produto).LoadAsync();
        await context.Entry(item).Reference(i => i.UnidadeMedida).LoadAsync();

        return ToResponse(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await context.Set<PedidoCompraItem>().FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return false;

        context.Set<PedidoCompraItem>().Remove(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir o item pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<PedidoCompraItemResponse>> GetAllAsync()
    {
        var itens = await context.Set<PedidoCompraItem>()
            .AsNoTracking()
            .Include(i => i.Produto)
            .Include(i => i.UnidadeMedida)
            .OrderByDescending(i => i.CriadoEm)
            .ToListAsync();

        return itens.Select(ToResponse);
    }

    public async Task<PedidoCompraItemResponse?> GetByIdAsync(int id)
    {
        var item = await context.Set<PedidoCompraItem>()
            .AsNoTracking()
            .Include(i => i.Produto)
            .Include(i => i.UnidadeMedida)
            .FirstOrDefaultAsync(i => i.Id == id);

        return item == null ? null : ToResponse(item);
    }

    public async Task<PedidoCompraItemResponse?> UpdateAsync(int id, PedidoCompraItemUpdateRequest request)
    {
        var item = await context.Set<PedidoCompraItem>().FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return null;

        if (item.PedidoCompraId != request.PedidoCompraId)
            await ValidatePedidoAsync(request.PedidoCompraId);

        if (item.ProdutoId != request.ProdutoId)
            await ValidateProdutoAsync(request.ProdutoId);

        if (item.UnidadeMedidaId != request.UnidadeMedidaId)
            await ValidateUnidadeMedidaAsync(request.UnidadeMedidaId);

        item.PedidoCompraId = request.PedidoCompraId;
        item.ProdutoId = request.ProdutoId;
        item.UnidadeMedidaId = request.UnidadeMedidaId;
        item.CotacaoItemId = request.CotacaoItemId;
        item.QuantidadeSolicitada = request.QuantidadeSolicitada;
        item.QuantidadeRecebida = request.QuantidadeRecebida;
        item.ValorUnitario = request.ValorUnitario;
        item.Total = request.Total;
        item.Observacao = request.Observacao;
        item.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar item do pedido de compra.");
        }

        await context.Entry(item).Reference(i => i.Produto).LoadAsync();
        await context.Entry(item).Reference(i => i.UnidadeMedida).LoadAsync();

        return ToResponse(item);
    }

    private async Task ValidatePedidoAsync(int pedidoId)
    {
        if (!await context.Set<PedidoCompra>().AnyAsync(p => p.Id == pedidoId))
            throw new NotFoundException("PedidoCompra", pedidoId);
    }

    private async Task ValidateProdutoAsync(int produtoId)
    {
        if (!await context.Set<Produto>().AnyAsync(p => p.Id == produtoId))
            throw new NotFoundException("Produto", produtoId);
    }

    private async Task ValidateUnidadeMedidaAsync(int unidadeId)
    {
        if (!await context.Set<UnidadeMedida>().AnyAsync(u => u.Id == unidadeId))
            throw new NotFoundException("UnidadeMedida", unidadeId);
    }

    private static PedidoCompraItemResponse ToResponse(PedidoCompraItem i) => new(
        i.Id,
        i.PedidoCompraId,
        i.ProdutoId,
        i.Produto?.Nome ?? string.Empty,
        i.UnidadeMedidaId,
        i.UnidadeMedida?.Sigla ?? string.Empty,
        i.CotacaoItemId,
        i.QuantidadeSolicitada,
        i.QuantidadeRecebida,
        i.ValorUnitario,
        i.Total,
        i.Observacao,
        i.CriadoEm,
        i.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
