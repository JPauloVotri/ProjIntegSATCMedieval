namespace MedievalApi.Models
{
    public class PedidoCompraItem
    {
        public int Id { get; set; }
        public int PedidoCompraId { get; set; }
        public int ProdutoId { get; set; }
        public int UnidadeMedidaId { get; set; }
        public int? CotacaoItemId { get; set; }
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
