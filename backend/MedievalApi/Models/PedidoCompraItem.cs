namespace MedievalApi.Models
{
    public class PedidoCompraItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid PedidoCompraId { get; set; } = Guid.CreateVersion7();
        public Guid ProdutoId { get; set; } = Guid.CreateVersion7();
        public Guid UnidadeMedidaId { get; set; } = Guid.CreateVersion7();
        public Guid? CotacaoItemId { get; set; } = Guid.CreateVersion7();
        public decimal QuantidadeSolicitada { get; set; }
        public decimal QuantidadeRecebida { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Total { get; set; }
        public string? Observacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegações
        public PedidoCompra? PedidoCompra { get; set; }
        public Produto? Produto { get; set; }
        public UnidadeMedida? UnidadeMedida { get; set; }
        public CotacaoItem? CotacaoItem { get; set; }
    }
}
