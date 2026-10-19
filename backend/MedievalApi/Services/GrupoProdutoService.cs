using MedievalApi.Data;
using MedievalApi.DTOs.GrupoProduto;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class GrupoProdutoService(AppDbContext appDbContext) : IGrupoProdutoService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<GrupoProdutoResponse> CreateAsync(GrupoProdutoCreateRequest request)
    {
        var grupo = new GrupoProduto
        {
            Nome = request.Nome,
            Descricao = request.Descricao
        };

        context.Set<GrupoProduto>().Add(grupo);
        await context.SaveChangesAsync();

        return ToResponse(grupo);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var grupo = await context.Set<GrupoProduto>().FirstOrDefaultAsync(g => g.Id == id);
        if (grupo == null) return false;

        context.Set<GrupoProduto>().Remove(grupo);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir o grupo de produto pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<GrupoProdutoResponse>> GetAllAsync()
    {
        var grupos = await context.Set<GrupoProduto>()
            .AsNoTracking()
            .OrderBy(g => g.Nome)
            .ToListAsync();

        return grupos.Select(ToResponse);
    }

    public async Task<GrupoProdutoResponse?> GetByIdAsync(int id)
    {
        var grupo = await context.Set<GrupoProduto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        return grupo == null ? null : ToResponse(grupo);
    }

    public async Task<GrupoProdutoResponse?> UpdateAsync(int id, GrupoProdutoUpdateRequest request)
    {
        var grupo = await context.Set<GrupoProduto>().FirstOrDefaultAsync(g => g.Id == id);
        if (grupo == null) return null;

        grupo.Nome = request.Nome;
        grupo.Descricao = request.Descricao;
        grupo.Ativo = request.Ativo;
        grupo.AtualizadoEm = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return ToResponse(grupo);
    }

    private static GrupoProdutoResponse ToResponse(GrupoProduto g) => new(
        g.Id,
        g.Nome,
        g.Descricao,
        g.Ativo,
        g.CriadoEm,
        g.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
