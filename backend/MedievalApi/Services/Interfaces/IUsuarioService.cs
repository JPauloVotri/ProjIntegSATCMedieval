using MedievalApi.DTOs.Usuario;

namespace MedievalApi.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponse>> GetAllAsync();
    Task<UsuarioResponse?> GetByIdAsync(int id);
    Task<UsuarioResponse> CreateAsync(UsuarioCreateRequest request);
    Task<UsuarioResponse?> UpdateAsync(int id, UsuarioUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
