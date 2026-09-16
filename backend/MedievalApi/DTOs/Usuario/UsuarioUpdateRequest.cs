using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioUpdateRequest(
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "E-mail deve ter no máximo 150 caracteres.")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    string Email,

    [Required(ErrorMessage = "Grupo de usuário é obrigatório.")]
    [EnumDataType(typeof(GrupoUsuario), ErrorMessage = "Grupo de usuário inválido.")]
    GrupoUsuario GrupoUsuario,

    [Required(ErrorMessage = "Status é obrigatório.")]
    [EnumDataType(typeof(UsuarioStatus), ErrorMessage = "Status inválido.")]
    UsuarioStatus Status
);
