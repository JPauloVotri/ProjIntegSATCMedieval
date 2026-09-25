using MedievalApi.DTOs.Fornecedor;

namespace MedievalApi.Services.Interfaces;

public interface IFornecedorService
{
    Task<IEnumerable<FornecedorResponse>> GetAllAsync();
    Task<FornecedorResponse?> GetByIdAsync(int id);
    Task<FornecedorResponse> CreateAsync(FornecedorCreateRequest request);
    Task<FornecedorResponse?> UpdateAsync(int id, FornecedorUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}