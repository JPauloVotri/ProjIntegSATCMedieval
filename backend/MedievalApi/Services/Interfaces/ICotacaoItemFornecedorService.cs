using MedievalApi.DTOs.CotacaoItemFornecedor;

namespace MedievalApi.Services.Interfaces;

public interface ICotacaoItemFornecedorService
{
    Task<IEnumerable<CotacaoItemFornecedorResponse>> GetAllAsync();
    Task<CotacaoItemFornecedorResponse?> GetByIdAsync(int id);
    Task<CotacaoItemFornecedorResponse> CreateAsync(CotacaoItemFornecedorCreateRequest request);
    Task<CotacaoItemFornecedorResponse?> UpdateAsync(int id, CotacaoItemFornecedorUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
