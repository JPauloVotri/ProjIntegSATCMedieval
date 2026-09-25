using MedievalApi.DTOs.SaidaEstoqueItem;

namespace MedievalApi.Services.Interfaces;

public interface ISaidaEstoqueItemService
{
    Task<IEnumerable<SaidaEstoqueItemResponse>> GetAllAsync();
    Task<SaidaEstoqueItemResponse?> GetByIdAsync(int id);
    Task<SaidaEstoqueItemResponse> CreateAsync(SaidaEstoqueItemCreateRequest request);
    Task<SaidaEstoqueItemResponse?> UpdateAsync(int id, SaidaEstoqueItemUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
