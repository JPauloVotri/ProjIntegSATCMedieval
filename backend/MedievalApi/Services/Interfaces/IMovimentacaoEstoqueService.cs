using MedievalApi.DTOs.MovimentacaoEstoque;

namespace MedievalApi.Services.Interfaces;

public interface IMovimentacaoEstoqueService
{
    Task<IEnumerable<MovimentacaoEstoqueResponse>> GetAllAsync();
    Task<MovimentacaoEstoqueResponse?> GetByIdAsync(int id);
    Task<MovimentacaoEstoqueResponse> CreateAsync(MovimentacaoEstoqueCreateRequest request);
    Task<MovimentacaoEstoqueResponse?> UpdateAsync(int id, MovimentacaoEstoqueUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
