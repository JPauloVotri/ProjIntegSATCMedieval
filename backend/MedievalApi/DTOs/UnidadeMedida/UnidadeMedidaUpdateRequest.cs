using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.UnidadeMedida;

public record UnidadeMedidaUpdateRequest(
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "Sigla é obrigatória.")]
    [MaxLength(20, ErrorMessage = "Sigla deve ter no máximo 20 caracteres.")]
    string Sigla,

    [Required(ErrorMessage = "Tipo é obrigatório.")]
    [MaxLength(50, ErrorMessage = "Tipo deve ter no máximo 50 caracteres.")]
    string Tipo,

    bool Ativo
);
