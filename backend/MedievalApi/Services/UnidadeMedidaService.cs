using MedievalApi.Data;
using MedievalApi.DTOs.UnidadeMedida;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class UnidadeMedidaService(AppDbContext appDbContext) : IUnidadeMedidaService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<UnidadeMedidaResponse> CreateAsync(UnidadeMedidaCreateRequest request)
    {
        var unidade = new UnidadeMedida
        {
            Nome = request.Nome,
            Sigla = request.Sigla,
            Tipo = request.Tipo
        };

        context.Set<UnidadeMedida>().Add(unidade);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new BusinessException("Unidade de medida já cadastrada.");
        }

        return ToResponse(unidade);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var unidade = await context.Set<UnidadeMedida>().FirstOrDefaultAsync(u => u.Id == id);
        if (unidade == null) return false;

        context.Set<UnidadeMedida>().Remove(unidade);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir a unidade de medida pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<UnidadeMedidaResponse>> GetAllAsync()
    {
        var unidades = await context.Set<UnidadeMedida>()
            .AsNoTracking()
            .OrderBy(u => u.Nome)
            .ToListAsync();

        return unidades.Select(ToResponse);
    }

    public async Task<UnidadeMedidaResponse?> GetByIdAsync(int id)
    {
        var unidade = await context.Set<UnidadeMedida>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return unidade == null ? null : ToResponse(unidade);
    }

    public async Task<UnidadeMedidaResponse?> UpdateAsync(int id, UnidadeMedidaUpdateRequest request)
    {
        var unidade = await context.Set<UnidadeMedida>().FirstOrDefaultAsync(u => u.Id == id);
        if (unidade == null) return null;

        unidade.Nome = request.Nome;
        unidade.Sigla = request.Sigla;
        unidade.Tipo = request.Tipo;
        unidade.Ativo = request.Ativo;
        unidade.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new BusinessException("Unidade de medida já cadastrada.");
        }

        return ToResponse(unidade);
    }

    private static UnidadeMedidaResponse ToResponse(UnidadeMedida u) => new(
        u.Id,
        u.Nome,
        u.Sigla,
        u.Tipo,
        u.Ativo,
        u.CriadoEm,
        u.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("duplicate key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23505", StringComparison.OrdinalIgnoreCase);
    }
}
