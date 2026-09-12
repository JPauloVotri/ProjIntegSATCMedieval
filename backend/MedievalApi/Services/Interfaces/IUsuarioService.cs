using MedievalApi.DTOs.Usuario;

namespace MedievalApi.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponse>> GetAllAsync();
    Task<UsuarioResponse?> GetByIdAsync(Guid id);
    Task<UsuarioResponse> CreateAsync(UsuarioCreateRequest request);
    Task<UsuarioResponse?> UpdateAsync(Guid id, UsuarioUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
