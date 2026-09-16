using MedievalApi.DTOs.Produto;

namespace MedievalApi.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponse>> GetAllAsync();
    Task<ProdutoResponse?> GetByIdAsync(Guid id);
    Task<ProdutoResponse> CreateAsync(ProdutoCreateRequest request);
    Task<ProdutoResponse?> UpdateAsync(Guid id, ProdutoUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
