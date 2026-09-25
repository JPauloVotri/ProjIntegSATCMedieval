using MedievalApi.DTOs.SaidaEstoque;

namespace MedievalApi.Services.Interfaces;

public interface ISaidaEstoqueService
{
    Task<IEnumerable<SaidaEstoqueResponse>> GetAllAsync();
    Task<SaidaEstoqueResponse?> GetByIdAsync(int id);
    Task<SaidaEstoqueResponse> CreateAsync(SaidaEstoqueCreateRequest request);
    Task<SaidaEstoqueResponse?> UpdateAsync(int id, SaidaEstoqueUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
