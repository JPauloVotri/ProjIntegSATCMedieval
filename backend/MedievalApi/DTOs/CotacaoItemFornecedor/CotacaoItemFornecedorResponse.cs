namespace MedievalApi.DTOs.CotacaoItemFornecedor;

public record CotacaoItemFornecedorResponse(
    int Id,
    int CotacaoItemId,
    int FornecedorId,
    string FornecedorNome,
    decimal ValorUnitario,
    int? PrazoEntregaDias,
    string? CondicaoPagamento,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
