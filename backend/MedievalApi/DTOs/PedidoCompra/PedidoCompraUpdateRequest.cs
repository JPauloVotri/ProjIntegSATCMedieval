using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.PedidoCompra;

public record PedidoCompraUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Fornecedor é obrigatório.")]
    int FornecedorId,

    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    int? CotacaoId,

    [Required(ErrorMessage = "Data do pedido é obrigatória.")]
    DateTime DataPedido,

    DateTime? DataPrevistaEntrega,

    DateTime? DataRecebimento,

    [EnumDataType(typeof(StatusPedidoCompra), ErrorMessage = "Status inválido.")]
    StatusPedidoCompra Status,

    [Range(0, double.MaxValue, ErrorMessage = "Total não pode ser negativo.")]
    decimal Total,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
