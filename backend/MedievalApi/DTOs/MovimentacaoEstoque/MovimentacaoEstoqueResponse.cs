using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.MovimentacaoEstoque;

public record MovimentacaoEstoqueResponse(
    int Id,
    int EstoqueId,
    int UsuarioId,
    string UsuarioNome,
    int? ReferenciaId,
    string? ReferenciaNome,
    TipoMovimentacaoEstoque Tipo,
    OrigemMovimentacao Origem,
    decimal Quantidade,
    decimal QuantidadeAnterior,
    decimal QuantidadePosterior,
    decimal? CustoUnitario,
    decimal? ValorTotal,
    string? Lote,
    DateTime? DataValidade,
    string? Observacao,
    DateTime CriadoEm
);
