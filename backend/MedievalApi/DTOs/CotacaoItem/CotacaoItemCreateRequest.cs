using MedievalApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.CotacaoItem;

public record CotacaoItemCreateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Cotação é obrigatória.")]
    int CotacaoId,

    [Range(1, int.MaxValue, ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Range(1, int.MaxValue, ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    decimal Quantidade,

    [Required(ErrorMessage = "Status é obrigatório.")]
    StatusCotacaoItem Status,

    [MaxLength(255, ErrorMessage = "Observação deve ter no máximo 255 caracteres.")]
    string? Observacao
);
