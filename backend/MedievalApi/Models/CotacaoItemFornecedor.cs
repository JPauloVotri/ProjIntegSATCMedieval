namespace MedievalApi.Models
{
    public class CotacaoItemFornecedor
    {
        public int Id { get; set; }

        public int CotacaoItemId { get; set; }
        public int FornecedorId { get; set; }

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
