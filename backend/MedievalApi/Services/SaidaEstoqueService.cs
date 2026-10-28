using MedievalApi.Data;
using MedievalApi.DTOs.SaidaEstoque;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class SaidaEstoqueService(AppDbContext appDbContext) : ISaidaEstoqueService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<SaidaEstoqueResponse> CreateAsync(SaidaEstoqueCreateRequest request)
    {
        await ValidateUsuarioAsync(request.UsuarioId);

        var saida = new SaidaEstoque
        {
            UsuarioId = request.UsuarioId,
            DataSaida = request.DataSaida ?? DateTime.UtcNow,
            Tipo = request.Tipo,
            Status = request.Status,
            Motivo = request.Motivo,
            Observacao = request.Observacao
        };

        context.Set<SaidaEstoque>().Add(saida);
        await SaveChangesAsync("criar saída de estoque");
        await context.Entry(saida).Reference(s => s.Usuario).LoadAsync();

        return ToResponse(saida);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var saida = await context.Set<SaidaEstoque>().FirstOrDefaultAsync(s => s.Id == id);
        if (saida is null) return false;

        context.Set<SaidaEstoque>().Remove(saida);
        await SaveChangesAsync("excluir saída de estoque");
        return true;
    }

    public async Task<IEnumerable<SaidaEstoqueResponse>> GetAllAsync()
    {
        var saidas = await context.Set<SaidaEstoque>()
            .AsNoTracking()
            .Include(s => s.Usuario)
            .OrderByDescending(s => s.DataSaida)
            .ToListAsync();

        return saidas.Select(ToResponse);
    }

    public async Task<SaidaEstoqueResponse?> GetByIdAsync(int id)
    {
        var saida = await context.Set<SaidaEstoque>()
            .AsNoTracking()
            .Include(s => s.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);

        return saida is null ? null : ToResponse(saida);
    }

    public async Task<SaidaEstoqueResponse?> UpdateAsync(int id, SaidaEstoqueUpdateRequest request)
    {
        var saida = await context.Set<SaidaEstoque>().FirstOrDefaultAsync(s => s.Id == id);
        if (saida is null) return null;

        if (saida.UsuarioId != request.UsuarioId)
            await ValidateUsuarioAsync(request.UsuarioId);

        saida.UsuarioId = request.UsuarioId;
        saida.DataSaida = request.DataSaida ?? saida.DataSaida;
        saida.Tipo = request.Tipo;
        saida.Status = request.Status;
        saida.Motivo = request.Motivo;
        saida.Observacao = request.Observacao;
        saida.AtualizadoEm = DateTime.UtcNow;

        await SaveChangesAsync("atualizar saída de estoque");
        await context.Entry(saida).Reference(s => s.Usuario).LoadAsync();

        return ToResponse(saida);
    }

    private async Task ValidateUsuarioAsync(int usuarioId)
    {
        if (!await context.Usuarios.AnyAsync(u => u.Id == usuarioId))
            throw new NotFoundException("Usuário", usuarioId);
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

    private static SaidaEstoqueResponse ToResponse(SaidaEstoque s) => new(
        s.Id,
        s.UsuarioId,
        s.Usuario?.Nome ?? string.Empty,
        s.DataSaida,
        s.Tipo,
        s.Status,
        s.Motivo,
        s.Observacao,
        s.CriadoEm,
        s.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
