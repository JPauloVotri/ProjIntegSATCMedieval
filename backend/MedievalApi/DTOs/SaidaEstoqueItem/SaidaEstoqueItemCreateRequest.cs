using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.SaidaEstoqueItem;

public record SaidaEstoqueItemCreateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Saída de estoque é obrigatória.")]
    int SaidaEstoqueId,

    [Range(1, int.MaxValue, ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Range(1, int.MaxValue, ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    decimal Quantidade,

    [Range(0, double.MaxValue, ErrorMessage = "Custo unitário não pode ser negativo.")]
    decimal CustoUnitario,

    [MaxLength(100, ErrorMessage = "Lote deve ter no máximo 100 caracteres.")]
    string? Lote,

    DateTime? DataValidade,

    [MaxLength(255, ErrorMessage = "Observação deve ter no máximo 255 caracteres.")]
    string? Observacao
);
