using MedievalApi.Models.Enums;

namespace MedievalApi.Models
{
    public class CotacaoItem
    {
        public int Id { get; set; }
        public int CotacaoId { get; set; }
        public int ProdutoId { get; set; }
        public int UnidadeMedidaId { get; set; }
        public decimal Quantidade { get; set; }
        public int? CotacaoItemFornecedorEscolhidoId { get; set; }
        public StatusCotacaoItem Status { get; set; } = StatusCotacaoItem.Pendente;
        public string? Observacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegações
        public Cotacao? Cotacao { get; set; }
        public Produto? Produto { get; set; }
        public UnidadeMedida? UnidadeMedida { get; set; }
        public CotacaoItemFornecedor? CotacaoItemFornecedorEscolhido { get; set; }
    }
}