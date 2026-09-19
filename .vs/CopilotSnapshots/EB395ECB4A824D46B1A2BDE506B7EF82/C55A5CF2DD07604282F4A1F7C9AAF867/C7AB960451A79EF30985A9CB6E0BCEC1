using MedievalApi.Data;
using MedievalApi.DTOs.Cotacao;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Models.Enums;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class CotacaoService(AppDbContext appDbContext) : ICotacaoService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<CotacaoResponse> CreateAsync(CotacaoCreateRequest request)
    {
        await ValidateUsuarioAsync(request.UsuarioId);

        var cotacao = new Cotacao
        {
            UsuarioId = request.UsuarioId,
            DataSolicitacao = request.DataSolicitacao,
            DataLimiteResposta = request.DataLimiteResposta,
            Status = request.Status,
            Observacao = request.Observacao
        };

        context.Set<Cotacao>().Add(cotacao);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar cotação.");
        }

        return ToResponse(cotacao);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cotacao = await context.Set<Cotacao>().FirstOrDefaultAsync(c => c.Id == id);
        if (cotacao == null) return false;

        if (cotacao.Status != StatusCotacao.Rascunho && cotacao.Status != StatusCotacao.Cancelada)
            throw new BusinessException("Só é possível excluir cotações em rascunho ou canceladas.");

        context.Set<Cotacao>().Remove(cotacao);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir a cotação pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<CotacaoResponse>> GetAllAsync()
    {
        var cotacoes = await context.Set<Cotacao>()
            .AsNoTracking()
            .OrderByDescending(c => c.DataSolicitacao)
            .ToListAsync();

        return cotacoes.Select(ToResponse);
    }

    public async Task<CotacaoResponse?> GetByIdAsync(int id)
    {
        var cotacao = await context.Set<Cotacao>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return cotacao == null ? null : ToResponse(cotacao);
    }

    public async Task<CotacaoResponse?> UpdateAsync(int id, CotacaoUpdateRequest request)
    {
        var cotacao = await context.Set<Cotacao>().FirstOrDefaultAsync(c => c.Id == id);
        if (cotacao == null) return null;

        // Only allow updates while in Rascunho
        if (cotacao.Status != StatusCotacao.Rascunho)
            throw new BusinessException("Só é possível atualizar cotações em rascunho.");

        if (cotacao.UsuarioId != request.UsuarioId)
        {
            await ValidateUsuarioAsync(request.UsuarioId);
        }

        cotacao.UsuarioId = request.UsuarioId;
        cotacao.DataSolicitacao = request.DataSolicitacao;
        cotacao.DataLimiteResposta = request.DataLimiteResposta;
        cotacao.Status = request.Status;
        cotacao.Observacao = request.Observacao;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar cotação.");
        }

        return ToResponse(cotacao);
    }

    private async Task ValidateUsuarioAsync(int usuarioId)
    {
        if (!await context.Usuarios.AnyAsync(u => u.Id == usuarioId))
            throw new NotFoundException("Usuario", usuarioId);
    }

    private static CotacaoResponse ToResponse(Cotacao c) => new(
        c.Id,
        c.UsuarioId,
        c.DataSolicitacao,
        c.DataLimiteResposta,
        c.Status,
        c.Observacao,
        c.CriadoEm,
        c.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
