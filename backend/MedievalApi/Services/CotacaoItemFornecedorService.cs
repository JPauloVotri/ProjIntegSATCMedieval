using MedievalApi.Data;
using MedievalApi.DTOs.CotacaoItemFornecedor;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Models.Enums;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class CotacaoItemFornecedorService(AppDbContext appDbContext) : ICotacaoItemFornecedorService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<IEnumerable<CotacaoItemFornecedorResponse>> GetAllAsync()
    {
        var itens = await context.Set<CotacaoItemFornecedor>()
            .AsNoTracking()
            .Include(i => i.Fornecedor)
            .OrderByDescending(i => i.CriadoEm)
            .ToListAsync();

        return itens.Select(ToResponse);
    }

    public async Task<CotacaoItemFornecedorResponse?> GetByIdAsync(int id)
    {
        var item = await context.Set<CotacaoItemFornecedor>()
            .AsNoTracking()
            .Include(i => i.Fornecedor)
            .FirstOrDefaultAsync(i => i.Id == id);

        return item == null ? null : ToResponse(item);
    }

    public async Task<CotacaoItemFornecedorResponse> CreateAsync(CotacaoItemFornecedorCreateRequest request)
    {
        await ValidateCotacaoItemAsync(request.CotacaoItemId);
        await ValidateFornecedorAsync(request.FornecedorId);

        var item = new CotacaoItemFornecedor
        {
            CotacaoItemId = request.CotacaoItemId,
            FornecedorId = request.FornecedorId,
            ValorUnitario = request.ValorUnitario,
            PrazoEntregaDias = request.PrazoEntregaDias,
            CondicaoPagamento = request.CondicaoPagamento,
            Observacao = request.Observacao
        };

        context.Set<CotacaoItemFornecedor>().Add(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException("Fornecedor já cadastrado para este item da cotação.");
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar oferta do fornecedor.");
        }

        // Reload navigation for response
        await context.Entry(item).Reference(i => i.Fornecedor).LoadAsync();

        return ToResponse(item);
    }

    public async Task<CotacaoItemFornecedorResponse?> UpdateAsync(int id, CotacaoItemFornecedorUpdateRequest request)
    {
        var item = await context.Set<CotacaoItemFornecedor>()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null) return null;

        if (item.Status != StatusCotacaoItemFornecedor.Pendente)
            throw new BusinessException("Só é possível atualizar ofertas de fornecedores pendentes.");

        if (item.CotacaoItemId != request.CotacaoItemId)
            await ValidateCotacaoItemAsync(request.CotacaoItemId);

        if (item.FornecedorId != request.FornecedorId)
            await ValidateFornecedorAsync(request.FornecedorId);

        item.CotacaoItemId = request.CotacaoItemId;
        item.FornecedorId = request.FornecedorId;
        item.ValorUnitario = request.ValorUnitario;
        item.PrazoEntregaDias = request.PrazoEntregaDias;
        item.CondicaoPagamento = request.CondicaoPagamento;
        item.Status = request.Status;
        item.Observacao = request.Observacao;
        item.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            throw new ConflictException("Fornecedor já cadastrado para este item da cotação.");
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar oferta do fornecedor.");
        }

        await context.Entry(item).Reference(i => i.Fornecedor).LoadAsync();

        return ToResponse(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await context.Set<CotacaoItemFornecedor>()
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null) return false;

        if (item.Status != StatusCotacaoItemFornecedor.Pendente)
            throw new BusinessException("Só é possível excluir ofertas de fornecedores pendentes.");

        context.Set<CotacaoItemFornecedor>().Remove(item);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir a oferta pois existem registros vinculados.");
        }

        return true;
    }

    private async Task ValidateCotacaoItemAsync(int cotacaoItemId)
    {
        if (!await context.Set<CotacaoItem>().AnyAsync(c => c.Id == cotacaoItemId))
            throw new NotFoundException("CotacaoItem", cotacaoItemId);
    }

    private async Task ValidateFornecedorAsync(int fornecedorId)
    {
        if (!await context.Set<Fornecedor>().AnyAsync(f => f.Id == fornecedorId))
            throw new NotFoundException("Fornecedor", fornecedorId);
    }

    private static CotacaoItemFornecedorResponse ToResponse(CotacaoItemFornecedor item) => new(
        item.Id,
        item.CotacaoItemId,
        item.FornecedorId,
        item.Fornecedor?.NomeFantasia ?? item.Fornecedor?.RazaoSocial ?? string.Empty,
        item.ValorUnitario,
        item.PrazoEntregaDias,
        item.CondicaoPagamento,
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

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("duplicate key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23505", StringComparison.OrdinalIgnoreCase);
    }
}