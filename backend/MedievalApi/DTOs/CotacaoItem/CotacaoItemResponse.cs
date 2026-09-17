using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.CotacaoItem;

public record CotacaoItemResponse(
    int Id,
    int CotacaoId,
    int ProdutoId,
    int UnidadeMedidaId,
    decimal Quantidade,
    int? CotacaoItemFornecedorEscolhidoId,
    StatusCotacaoItem Status,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
