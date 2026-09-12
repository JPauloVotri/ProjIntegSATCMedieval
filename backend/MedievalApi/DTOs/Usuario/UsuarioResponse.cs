using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioResponse(
    Guid Id,
    string Nome,
    string Email,
    GrupoUsuario GrupoUsuario,
    UsuarioStatus Status,
    DateTime? UltimoLogin,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
