namespace MedievalApi.DTOs.SaidaEstoqueItem;

public record SaidaEstoqueItemResponse(
    int Id,
    int SaidaEstoqueId,
    int ProdutoId,
    string ProdutoNome,
    int UnidadeMedidaId,
    string UnidadeMedidaSigla,
    decimal Quantidade,
    decimal CustoUnitario,
    decimal CustoTotal,
    string? Lote,
    DateTime? DataValidade,
    string? Observacao,
    DateTime CriadoEm
);
