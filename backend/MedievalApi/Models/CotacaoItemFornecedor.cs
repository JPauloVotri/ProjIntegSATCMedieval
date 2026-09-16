namespace MedievalApi.Models
{
    public class CotacaoItemFornecedor
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        public Guid CotacaoItemId { get; set; } = Guid.CreateVersion7();
        public Guid FornecedorId { get; set; } = Guid.CreateVersion7();

        public decimal ValorUnitario { get; set; }

        public int? PrazoEntregaDias { get; set; }

        public string? CondicaoPagamento { get; set; }
        public string? Observacao { get; set; }

        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegações
        public CotacaoItem? CotacaoItem { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }


}
