using MedievalApi.Data;
using MedievalApi.DTOs.PedidoCompra;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class PedidoCompraService(AppDbContext appDbContext) : IPedidoCompraService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<PedidoCompraResponse> CreateAsync(PedidoCompraCreateRequest request)
    {
        await ValidateFornecedorAsync(request.FornecedorId);
        await ValidateUsuarioAsync(request.UsuarioId);

        var pedido = new PedidoCompra
        {
            FornecedorId = request.FornecedorId,
            UsuarioId = request.UsuarioId,
            CotacaoId = request.CotacaoId,
            DataPedido = request.DataPedido,
            DataPrevistaEntrega = request.DataPrevistaEntrega,
            DataRecebimento = request.DataRecebimento,
            Status = request.Status,
            Total = request.Total,
            Observacao = request.Observacao
        };

        context.Set<PedidoCompra>().Add(pedido);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar pedido de compra.");
        }

        await context.Entry(pedido).Reference(p => p.Fornecedor).LoadAsync();
        await context.Entry(pedido).Reference(p => p.Usuario).LoadAsync();

        return ToResponse(pedido);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pedido = await context.Set<PedidoCompra>().FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null) return false;

        if (pedido.Status != Models.Enums.StatusPedidoCompra.Rascunho)
            throw new BusinessException("Só é possível excluir pedidos em rascunho.");

        context.Set<PedidoCompra>().Remove(pedido);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir o pedido pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<PedidoCompraResponse>> GetAllAsync()
    {
        var pedidos = await context.Set<PedidoCompra>()
            .AsNoTracking()
            .Include(p => p.Fornecedor)
            .Include(p => p.Usuario)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();

        return pedidos.Select(ToResponse);
    }

    public async Task<PedidoCompraResponse?> GetByIdAsync(int id)
    {
        var pedido = await context.Set<PedidoCompra>()
            .AsNoTracking()
            .Include(p => p.Fornecedor)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

        return pedido == null ? null : ToResponse(pedido);
    }

    public async Task<PedidoCompraResponse?> UpdateAsync(int id, PedidoCompraUpdateRequest request)
    {
        var pedido = await context.Set<PedidoCompra>().FirstOrDefaultAsync(p => p.Id == id);
        if (pedido == null) return null;

        if (pedido.FornecedorId != request.FornecedorId)
            await ValidateFornecedorAsync(request.FornecedorId);

        if (pedido.UsuarioId != request.UsuarioId)
            await ValidateUsuarioAsync(request.UsuarioId);

        pedido.FornecedorId = request.FornecedorId;
        pedido.UsuarioId = request.UsuarioId;
        pedido.CotacaoId = request.CotacaoId;
        pedido.DataPedido = request.DataPedido;
        pedido.DataPrevistaEntrega = request.DataPrevistaEntrega;
        pedido.DataRecebimento = request.DataRecebimento;
        pedido.Status = request.Status;
        pedido.Total = request.Total;
        pedido.Observacao = request.Observacao;
        pedido.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar pedido de compra.");
        }

        await context.Entry(pedido).Reference(p => p.Fornecedor).LoadAsync();
        await context.Entry(pedido).Reference(p => p.Usuario).LoadAsync();

        return ToResponse(pedido);
    }

    private async Task ValidateFornecedorAsync(int fornecedorId)
    {
        if (!await context.Fornecedores.AnyAsync(f => f.Id == fornecedorId))
            throw new NotFoundException("Fornecedor", fornecedorId);
    }

    private async Task ValidateUsuarioAsync(int usuarioId)
    {
        if (!await context.Usuarios.AnyAsync(u => u.Id == usuarioId))
            throw new NotFoundException("Usuario", usuarioId);
    }

    private static PedidoCompraResponse ToResponse(PedidoCompra p) => new(
        p.Id,
        p.FornecedorId,
        p.Fornecedor?.RazaoSocial ?? string.Empty,
        p.UsuarioId,
        p.Usuario?.Nome ?? string.Empty,
        p.CotacaoId,
        p.DataPedido,
        p.DataPrevistaEntrega,
        p.DataRecebimento,
        p.Status,
        p.Total,
        p.Observacao,
        p.CriadoEm,
        p.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
