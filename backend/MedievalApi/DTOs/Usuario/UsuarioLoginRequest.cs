
using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.Usuario;

public class UsuarioLoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Senha { get; set; } = string.Empty;
}
