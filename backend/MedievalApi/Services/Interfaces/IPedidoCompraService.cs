using MedievalApi.DTOs.PedidoCompra;

namespace MedievalApi.Services.Interfaces;

public interface IPedidoCompraService
{
    Task<IEnumerable<PedidoCompraResponse>> GetAllAsync();
    Task<PedidoCompraResponse?> GetByIdAsync(int id);
    Task<PedidoCompraResponse> CreateAsync(PedidoCompraCreateRequest request);
    Task<PedidoCompraResponse?> UpdateAsync(int id, PedidoCompraUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
