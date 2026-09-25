using MedievalApi.DTOs.UnidadeMedida;

namespace MedievalApi.Services.Interfaces;

public interface IUnidadeMedidaService
{
    Task<IEnumerable<UnidadeMedidaResponse>> GetAllAsync();
    Task<UnidadeMedidaResponse?> GetByIdAsync(int id);
    Task<UnidadeMedidaResponse> CreateAsync(UnidadeMedidaCreateRequest request);
    Task<UnidadeMedidaResponse?> UpdateAsync(int id, UnidadeMedidaUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
