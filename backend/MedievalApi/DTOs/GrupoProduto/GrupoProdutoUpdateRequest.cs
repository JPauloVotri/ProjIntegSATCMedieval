using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.GrupoProduto;

public record GrupoProdutoUpdateRequest(
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres.")]
    string Nome,

    [MaxLength(255, ErrorMessage = "Descrição deve ter no máximo 255 caracteres.")]
    string? Descricao,

    bool Ativo
);
