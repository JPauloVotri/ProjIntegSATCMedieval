using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.CotacaoItem;

public record CotacaoItemResponse(
    int Id,
    int CotacaoId,
    int ProdutoId,
    string ProdutoNome,
    int UnidadeMedidaId,
    string UnidadeMedidaSigla,
    decimal Quantidade,
    StatusCotacaoItem Status,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
