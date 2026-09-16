namespace MedievalApi.Models
{
    public class SaidaEstoqueItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid SaidaEstoqueId { get; set; } = Guid.CreateVersion7();
        public Guid ProdutoId { get; set; } = Guid.CreateVersion7();
        public Guid UnidadeMedidaId { get; set; } = Guid.CreateVersion7();
        public decimal Quantidade { get; set; }
        public decimal CustoUnitario { get; set; }
        public decimal CustoTotal { get; set; }
        public string? Lote { get; set; }
        public DateTime? DataValidade { get; set; }
        public string? Observacao { get; set; }
        public DateTime CriadoEm { get; set; }

        // Navegações
        public SaidaEstoque? SaidaEstoque { get; set; }
        public Produto? Produto { get; set; }
        public UnidadeMedida? UnidadeMedida { get; set; }
    }
}
