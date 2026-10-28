using MedievalApi.Data;
using MedievalApi.DTOs.SaidaEstoqueItem;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class SaidaEstoqueItemService(AppDbContext appDbContext) : ISaidaEstoqueItemService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<SaidaEstoqueItemResponse> CreateAsync(SaidaEstoqueItemCreateRequest request)
    {
        await ValidateReferencesAsync(request.SaidaEstoqueId, request.ProdutoId, request.UnidadeMedidaId);

        var item = new SaidaEstoqueItem
        {
            SaidaEstoqueId = request.SaidaEstoqueId,
            ProdutoId = request.ProdutoId,
            UnidadeMedidaId = request.UnidadeMedidaId,
            Quantidade = request.Quantidade,
            CustoUnitario = request.CustoUnitario,
            CustoTotal = request.Quantidade * request.CustoUnitario,
            Lote = request.Lote,
            DataValidade = request.DataValidade,
            Observacao = request.Observacao
        };

        context.Set<SaidaEstoqueItem>().Add(item);
        await SaveChangesAsync("criar item de saída de estoque");
        await LoadReferencesAsync(item);

        return ToResponse(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await context.Set<SaidaEstoqueItem>().FirstOrDefaultAsync(i => i.Id == id);
        if (item is null) return false;

        context.Set<SaidaEstoqueItem>().Remove(item);
        await SaveChangesAsync("excluir item de saída de estoque");
        return true;
    }

    public async Task<IEnumerable<SaidaEstoqueItemResponse>> GetAllAsync()
    {
        var itens = await Query()
            .AsNoTracking()
            .OrderBy(i => i.SaidaEstoqueId)
            .ThenBy(i => i.Id)
            .ToListAsync();

        return itens.Select(ToResponse);
    }

    public async Task<SaidaEstoqueItemResponse?> GetByIdAsync(int id)
    {
        var item = await Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        return item is null ? null : ToResponse(item);
    }

    public async Task<SaidaEstoqueItemResponse?> UpdateAsync(int id, SaidaEstoqueItemUpdateRequest request)
    {
        var item = await context.Set<SaidaEstoqueItem>().FirstOrDefaultAsync(i => i.Id == id);
        if (item is null) return null;

        if (item.SaidaEstoqueId != request.SaidaEstoqueId
            || item.ProdutoId != request.ProdutoId
            || item.UnidadeMedidaId != request.UnidadeMedidaId)
        {
            await ValidateReferencesAsync(request.SaidaEstoqueId, request.ProdutoId, request.UnidadeMedidaId);
        }

        item.SaidaEstoqueId = request.SaidaEstoqueId;
        item.ProdutoId = request.ProdutoId;
        item.UnidadeMedidaId = request.UnidadeMedidaId;
        item.Quantidade = request.Quantidade;
        item.CustoUnitario = request.CustoUnitario;
        item.CustoTotal = request.CustoTotal;
        item.Lote = request.Lote;
        item.DataValidade = request.DataValidade;
        item.Observacao = request.Observacao;

        await SaveChangesAsync("atualizar item de saída de estoque");
        await LoadReferencesAsync(item);

        return ToResponse(item);
    }

    private IQueryable<SaidaEstoqueItem> Query() => context.Set<SaidaEstoqueItem>()
        .Include(i => i.Produto)
        .Include(i => i.UnidadeMedida);

    private async Task ValidateReferencesAsync(int saidaEstoqueId, int produtoId, int unidadeMedidaId)
    {
        if (!await context.SaidaEstoques.AnyAsync(s => s.Id == saidaEstoqueId))
            throw new NotFoundException("Saída de estoque", saidaEstoqueId);
        if (!await context.Produtos.AnyAsync(p => p.Id == produtoId))
            throw new NotFoundException("Produto", produtoId);
        if (!await context.UnidadesMedida.AnyAsync(u => u.Id == unidadeMedidaId))
            throw new NotFoundException("Unidade de medida", unidadeMedidaId);
    }

    private async Task LoadReferencesAsync(SaidaEstoqueItem item)
    {
        await context.Entry(item).Reference(i => i.Produto).LoadAsync();
        await context.Entry(item).Reference(i => i.UnidadeMedida).LoadAsync();
    }

    private async Task SaveChangesAsync(string operation)
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException($"Referência inválida ao {operation}.");
        }
    }

    private static SaidaEstoqueItemResponse ToResponse(SaidaEstoqueItem i) => new(
        i.Id,
        i.SaidaEstoqueId,
        i.ProdutoId,
        i.Produto?.Nome ?? string.Empty,
        i.UnidadeMedidaId,
        i.UnidadeMedida?.Sigla ?? string.Empty,
        i.Quantidade,
        i.CustoUnitario,
        i.CustoTotal,
        i.Lote,
        i.DataValidade,
        i.Observacao,
        i.CriadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
