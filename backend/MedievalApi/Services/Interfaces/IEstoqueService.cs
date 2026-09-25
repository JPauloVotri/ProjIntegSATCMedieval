using MedievalApi.DTOs.Estoque;

namespace MedievalApi.Services.Interfaces;

public interface IEstoqueService
{
    Task<IEnumerable<EstoqueResponse>> GetAllAsync();
    Task<EstoqueResponse?> GetByIdAsync(int id);
    Task<EstoqueResponse> CreateAsync(EstoqueCreateRequest request);
    Task<EstoqueResponse?> UpdateAsync(int id, EstoqueUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
