using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioResponse(
    int Id,
    string Nome,
    string Email,
    GrupoUsuario GrupoUsuario,
    StatusUsuario Status,
    DateTime? UltimoLogin,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
