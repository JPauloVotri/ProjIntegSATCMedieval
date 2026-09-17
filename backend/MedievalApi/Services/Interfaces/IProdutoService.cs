using MedievalApi.DTOs.Produto;

namespace MedievalApi.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponse>> GetAllAsync();
    Task<ProdutoResponse?> GetByIdAsync(int id);
    Task<ProdutoResponse> CreateAsync(ProdutoCreateRequest request);
    Task<ProdutoResponse?> UpdateAsync(int id, ProdutoUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
