using MedievalApi.DTOs.PedidoCompraItem;

namespace MedievalApi.Services.Interfaces;

public interface IPedidoCompraItemService
{
    Task<IEnumerable<PedidoCompraItemResponse>> GetAllAsync();
    Task<PedidoCompraItemResponse?> GetByIdAsync(int id);
    Task<PedidoCompraItemResponse> CreateAsync(PedidoCompraItemCreateRequest request);
    Task<PedidoCompraItemResponse?> UpdateAsync(int id, PedidoCompraItemUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
