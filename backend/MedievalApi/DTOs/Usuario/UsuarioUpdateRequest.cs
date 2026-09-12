using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioUpdateRequest(
    string Nome,
    string Email,
    GrupoUsuario GrupoUsuario,
    UsuarioStatus Status
);
