using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.PedidoCompraItem;

public record PedidoCompraItemCreateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Pedido de compra é obrigatório.")]
    int PedidoCompraId,

    [Range(1, int.MaxValue, ErrorMessage = "Produto é obrigatório.")]
    int ProdutoId,

    [Range(1, int.MaxValue, ErrorMessage = "Unidade de medida é obrigatória.")]
    int UnidadeMedidaId,

    int? CotacaoItemId,

    [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade solicitada deve ser maior que zero.")]
    decimal QuantidadeSolicitada,

    [Range(0, double.MaxValue, ErrorMessage = "Valor unitário não pode ser negativo.")]
    decimal ValorUnitario,

    [MaxLength(255, ErrorMessage = "Observação deve ter no máximo 255 caracteres.")]
    string? Observacao
);
