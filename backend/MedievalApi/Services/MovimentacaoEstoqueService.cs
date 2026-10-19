using MedievalApi.Data;
using MedievalApi.DTOs.MovimentacaoEstoque;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class MovimentacaoEstoqueService(AppDbContext appDbContext) : IMovimentacaoEstoqueService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<MovimentacaoEstoqueResponse> CreateAsync(MovimentacaoEstoqueCreateRequest request)
    {
        await ValidateEstoqueAsync(request.EstoqueId);
        await ValidateUsuarioAsync(request.UsuarioId);

        var m = new MovimentacaoEstoque
        {
            EstoqueId = request.EstoqueId,
            UsuarioId = request.UsuarioId,
            ReferenciaId = request.ReferenciaId,
            ReferenciaTipo = request.ReferenciaTipo,
            Tipo = request.Tipo,
            Origem = request.Origem,
            Quantidade = request.Quantidade,
            QuantidadeAnterior = request.QuantidadeAnterior,
            QuantidadePosterior = request.QuantidadePosterior,
            CustoUnitario = request.CustoUnitario,
            ValorTotal = request.ValorTotal,
            Lote = request.Lote,
            DataValidade = request.DataValidade,
            Observacao = request.Observacao
        };

        context.Set<MovimentacaoEstoque>().Add(m);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar movimentação de estoque.");
        }

        await context.Entry(m).Reference(x => x.Usuario).LoadAsync();
        await context.Entry(m).Reference(x => x.Estoque).LoadAsync();

        return ToResponse(m);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var m = await context.Set<MovimentacaoEstoque>().FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return false;

        context.Set<MovimentacaoEstoque>().Remove(m);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir a movimentação pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<MovimentacaoEstoqueResponse>> GetAllAsync()
    {
        var list = await context.Set<MovimentacaoEstoque>()
            .AsNoTracking()
            .Include(m => m.Usuario)
            .Include(m => m.Estoque)
            .OrderByDescending(m => m.CriadoEm)
            .ToListAsync();

        return list.Select(ToResponse);
    }

    public async Task<MovimentacaoEstoqueResponse?> GetByIdAsync(int id)
    {
        var m = await context.Set<MovimentacaoEstoque>()
            .AsNoTracking()
            .Include(mv => mv.Usuario)
            .Include(mv => mv.Estoque)
            .FirstOrDefaultAsync(mv => mv.Id == id);

        return m == null ? null : ToResponse(m);
    }

    public async Task<MovimentacaoEstoqueResponse?> UpdateAsync(int id, MovimentacaoEstoqueUpdateRequest request)
    {
        var m = await context.Set<MovimentacaoEstoque>().FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return null;

        if (m.EstoqueId != request.EstoqueId)
            await ValidateEstoqueAsync(request.EstoqueId);

        if (m.UsuarioId != request.UsuarioId)
            await ValidateUsuarioAsync(request.UsuarioId);

        m.EstoqueId = request.EstoqueId;
        m.UsuarioId = request.UsuarioId;
        m.ReferenciaId = request.ReferenciaId;
        m.ReferenciaTipo = request.ReferenciaTipo;
        m.Tipo = request.Tipo;
        m.Origem = request.Origem;
        m.Quantidade = request.Quantidade;
        m.QuantidadeAnterior = request.QuantidadeAnterior;
        m.QuantidadePosterior = request.QuantidadePosterior;
        m.CustoUnitario = request.CustoUnitario;
        m.ValorTotal = request.ValorTotal;
        m.Lote = request.Lote;
        m.DataValidade = request.DataValidade;
        m.Observacao = request.Observacao;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar movimentação de estoque.");
        }

        await context.Entry(m).Reference(x => x.Usuario).LoadAsync();
        await context.Entry(m).Reference(x => x.Estoque).LoadAsync();

        return ToResponse(m);
    }

    private async Task ValidateEstoqueAsync(int estoqueId)
    {
        if (!await context.Set<Estoque>().AnyAsync(e => e.Id == estoqueId))
            throw new NotFoundException("Estoque", estoqueId);
    }

    private async Task ValidateUsuarioAsync(int usuarioId)
    {
        if (!await context.Set<Usuario>().AnyAsync(u => u.Id == usuarioId))
            throw new NotFoundException("Usuario", usuarioId);
    }

    private static MovimentacaoEstoqueResponse ToResponse(MovimentacaoEstoque m) => new(
        m.Id,
        m.EstoqueId,
        m.UsuarioId,
        m.Usuario?.Nome ?? string.Empty,
        m.ReferenciaId,
        m.ReferenciaTipo,
        m.Tipo,
        m.Origem,
        m.Quantidade,
        m.QuantidadeAnterior,
        m.QuantidadePosterior,
        m.CustoUnitario,
        m.ValorTotal,
        m.Lote,
        m.DataValidade,
        m.Observacao,
        m.CriadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
