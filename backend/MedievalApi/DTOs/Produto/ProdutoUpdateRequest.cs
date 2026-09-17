using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.Produto;

public record ProdutoUpdateRequest(
    [Required(ErrorMessage = "Categoria é obrigatória.")]
    int CategoriaId,

    [Required(ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(150, ErrorMessage = "Nome deve ter no máximo 150 caracteres.")]
    string Nome,

    [MaxLength(50, ErrorMessage = "Código deve ter no máximo 50 caracteres.")]
    string? Codigo,

    [MaxLength(255, ErrorMessage = "Descrição deve ter no máximo 255 caracteres.")]
    string? Descricao,

    [Range(0, double.MaxValue, ErrorMessage = "Estoque mínimo não pode ser negativo.")]
    decimal EstoqueMinimo,

    [Range(0, double.MaxValue, ErrorMessage = "Estoque máximo não pode ser negativo.")]
    decimal? EstoqueMaximo,

    [Range(1, int.MaxValue, ErrorMessage = "Validade em dias deve ser maior que zero.")]
    int? ValidadeDias,

    [Required(ErrorMessage = "Situação ativo/inativo é obrigatória.")]
    bool Ativo
);
