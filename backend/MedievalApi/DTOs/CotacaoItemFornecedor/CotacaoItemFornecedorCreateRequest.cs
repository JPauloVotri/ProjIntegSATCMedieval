using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.CotacaoItemFornecedor;

public record CotacaoItemFornecedorCreateRequest(
    [Required(ErrorMessage = "Item da cotação é obrigatório.")]
    int CotacaoItemId,

    [Required(ErrorMessage = "Fornecedor é obrigatório.")]
    int FornecedorId,

    [Range(0, double.MaxValue, ErrorMessage = "Valor unitário não pode ser negativo.")]
    decimal ValorUnitario,

    [Range(0, int.MaxValue, ErrorMessage = "Prazo de entrega não pode ser negativo.")]
    int? PrazoEntregaDias,

    [MaxLength(100, ErrorMessage = "Condição de pagamento deve ter no máximo 100 caracteres.")]
    string? CondicaoPagamento,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
