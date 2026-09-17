using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.CotacaoItem;

public record CotacaoItemUpdateRequest(
    [Required(ErrorMessage = "Cotação é obrigatória.")]
    int CotacaoId,

    [Required(ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Required(ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    decimal Quantidade,

    int? CotacaoItemFornecedorEscolhidoId,

    [Required(ErrorMessage = "Status do item da cotação é obrigatório.")]
    StatusCotacaoItem Status,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
