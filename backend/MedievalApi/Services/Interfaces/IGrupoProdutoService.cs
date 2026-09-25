using MedievalApi.DTOs.GrupoProduto;

namespace MedievalApi.Services.Interfaces;

public interface IGrupoProdutoService
{
    Task<IEnumerable<GrupoProdutoResponse>> GetAllAsync();
    Task<GrupoProdutoResponse?> GetByIdAsync(int id);
    Task<GrupoProdutoResponse> CreateAsync(GrupoProdutoCreateRequest request);
    Task<GrupoProdutoResponse?> UpdateAsync(int id, GrupoProdutoUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
