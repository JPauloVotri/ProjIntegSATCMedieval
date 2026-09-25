using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.PedidoCompra;

public record PedidoCompraCreateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Fornecedor é obrigatório.")]
    int FornecedorId,

    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    int? CotacaoId,

    [Required(ErrorMessage = "Data do pedido é obrigatória.")]
    DateTime DataPedido,

    DateTime? DataPrevistaEntrega,

    [EnumDataType(typeof(StatusPedidoCompra), ErrorMessage = "Status inválido.")]
    StatusPedidoCompra Status,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
