using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioCreateRequest(
    string Nome,
    string Email,
    string Senha,
    GrupoUsuario GrupoUsuario
);
