using MedievalApi.DTOs.CotacaoItem;

namespace MedievalApi.Services.Interfaces;

public interface ICotacaoItemService
{
    Task<IEnumerable<CotacaoItemResponse>> GetAllAsync();
    Task<CotacaoItemResponse?> GetByIdAsync(int id);
    Task<CotacaoItemResponse> CreateAsync(CotacaoItemCreateRequest request);
    Task<CotacaoItemResponse?> UpdateAsync(int id, CotacaoItemUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
