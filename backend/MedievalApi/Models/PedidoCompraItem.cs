namespace MedievalApi.Models;

public class PedidoCompraItem
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid PedidoCompraId { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeMedidaId { get; set; }
    public Guid? CotacaoItemId { get; set; }
    public decimal QuantidadeSolicitada { get; set; }
    public decimal QuantidadeRecebida { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Total { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    // Navegações
    public PedidoCompra? PedidoCompra { get; set; }
    public Produto? Produto { get; set; }
    public UnidadeMedida? UnidadeMedida { get; set; }
    public CotacaoItem? CotacaoItem { get; set; }
}
