using MedievalApi.DTOs.Cotacao;

namespace MedievalApi.Services.Interfaces;

public interface ICotacaoService
{
    Task<IEnumerable<CotacaoResponse>> GetAllAsync();
    Task<CotacaoResponse?> GetByIdAsync(int id);
    Task<CotacaoResponse> CreateAsync(CotacaoCreateRequest request);
    Task<CotacaoResponse?> UpdateAsync(int id, CotacaoUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
