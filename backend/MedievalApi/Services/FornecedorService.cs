using MedievalApi.Data;
using MedievalApi.DTOs.Fornecedor;
using MedievalApi.Exceptions;
using MedievalApi.Models;
using MedievalApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedievalApi.Services;

public class FornecedorService(AppDbContext appDbContext) : IFornecedorService
{
    private readonly AppDbContext context = appDbContext;

    public async Task<FornecedorResponse> CreateAsync(FornecedorCreateRequest request)
    {
        var fornecedor = new Fornecedor
        {
            RazaoSocial = request.RazaoSocial,
            NomeFantasia = request.NomeFantasia,
            Documento = request.Documento,
            InscricaoEstadual = request.InscricaoEstadual,
            Email = request.Email,
            Telefone = request.Telefone,
            NomeContato = request.NomeContato,
            Cep = request.Cep,
            Logradouro = request.Logradouro,
            Numero = request.Numero,
            Complemento = request.Complemento,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Estado = request.Estado,
            DiasEntrega = request.DiasEntrega,
            CondicaoPagamento = request.CondicaoPagamento
        };

        context.Set<Fornecedor>().Add(fornecedor);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao criar fornecedor.");
        }

        return ToResponse(fornecedor);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var fornecedor = await context.Set<Fornecedor>().FirstOrDefaultAsync(f => f.Id == id);
        if (fornecedor == null) return false;

        context.Set<Fornecedor>().Remove(fornecedor);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Não é possível excluir o fornecedor pois existem registros vinculados.");
        }

        return true;
    }

    public async Task<IEnumerable<FornecedorResponse>> GetAllAsync()
    {
        var fornecedores = await context.Set<Fornecedor>()
            .AsNoTracking()
            .OrderBy(f => f.RazaoSocial)
            .ToListAsync();

        return fornecedores.Select(ToResponse);
    }

    public async Task<FornecedorResponse?> GetByIdAsync(int id)
    {
        var fornecedor = await context.Set<Fornecedor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);

        return fornecedor == null ? null : ToResponse(fornecedor);
    }

    public async Task<FornecedorResponse?> UpdateAsync(int id, FornecedorUpdateRequest request)
    {
        var fornecedor = await context.Set<Fornecedor>().FirstOrDefaultAsync(f => f.Id == id);
        if (fornecedor == null) return null;

        fornecedor.RazaoSocial = request.RazaoSocial;
        fornecedor.NomeFantasia = request.NomeFantasia;
        fornecedor.Documento = request.Documento;
        fornecedor.InscricaoEstadual = request.InscricaoEstadual;
        fornecedor.Email = request.Email;
        fornecedor.Telefone = request.Telefone;
        fornecedor.NomeContato = request.NomeContato;
        fornecedor.Cep = request.Cep;
        fornecedor.Logradouro = request.Logradouro;
        fornecedor.Numero = request.Numero;
        fornecedor.Complemento = request.Complemento;
        fornecedor.Bairro = request.Bairro;
        fornecedor.Cidade = request.Cidade;
        fornecedor.Estado = request.Estado;
        fornecedor.DiasEntrega = request.DiasEntrega;
        fornecedor.CondicaoPagamento = request.CondicaoPagamento;
        fornecedor.Ativo = request.Ativo;
        fornecedor.AtualizadoEm = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
        {
            throw new BusinessException("Referência inválida ao atualizar fornecedor.");
        }

        return ToResponse(fornecedor);
    }

    private static FornecedorResponse ToResponse(Fornecedor f) => new(
        f.Id,
        f.RazaoSocial,
        f.NomeFantasia,
        f.Documento,
        f.InscricaoEstadual,
        f.Email,
        f.Telefone,
        f.NomeContato,
        f.Cep,
        f.Logradouro,
        f.Numero,
        f.Complemento,
        f.Bairro,
        f.Cidade,
        f.Estado,
        f.DiasEntrega,
        f.CondicaoPagamento,
        f.Ativo,
        f.CriadoEm,
        f.AtualizadoEm
    );

    private static bool IsForeignKeyViolation(DbUpdateException ex)
    {
        var inner = ex.InnerException?.Message ?? string.Empty;
        return inner.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("foreign key", StringComparison.OrdinalIgnoreCase)
            || inner.Contains("23503", StringComparison.OrdinalIgnoreCase);
    }
}
