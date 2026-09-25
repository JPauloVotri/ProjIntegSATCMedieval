namespace MedievalApi.DTOs.PedidoCompraItem;

public record PedidoCompraItemResponse(
    int Id,
    int PedidoCompraId,
    int ProdutoId,
    string ProdutoNome,
    int UnidadeMedidaId,
    string UnidadeMedidaSigla,
    int? CotacaoItemId,
    decimal QuantidadeSolicitada,
    decimal QuantidadeRecebida,
    decimal ValorUnitario,
    decimal Total,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
