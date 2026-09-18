using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Usuario;

public record UsuarioCreateRequest(
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [MaxLength(150, ErrorMessage = "E-mail deve ter no máximo 150 caracteres.")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    string Email,

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres.")]
    string Senha,

    [EnumDataType(typeof(GrupoUsuario), ErrorMessage = "Grupo de usuário inválido.")]
    GrupoUsuario GrupoUsuario
);
