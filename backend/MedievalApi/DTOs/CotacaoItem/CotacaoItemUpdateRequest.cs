using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.CotacaoItem;

public record CotacaoItemUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Cotação é obrigatória.")]
    int CotacaoId,

    [Range(1, int.MaxValue, ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Range(1, int.MaxValue, ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    decimal Quantidade,

    [EnumDataType(typeof(StatusCotacaoItem), ErrorMessage = "Status inválido.")]
    StatusCotacaoItem Status,

    [MaxLength(255, ErrorMessage = "Observação deve ter no máximo 255 caracteres.")]
    string? Observacao
);
