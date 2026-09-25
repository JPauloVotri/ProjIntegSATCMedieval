using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.PedidoCompra;

public record PedidoCompraResponse(
    int Id,
    int FornecedorId,
    string FornecedorNome,
    int UsuarioId,
    string UsuarioNome,
    int? CotacaoId,
    DateTime DataPedido,
    DateTime? DataPrevistaEntrega,
    DateTime? DataRecebimento,
    StatusPedidoCompra Status,
    decimal Total,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
