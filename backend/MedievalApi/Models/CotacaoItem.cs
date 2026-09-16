using MedievalApi.Models.Enums;

namespace MedievalApi.Models
{
    public class CotacaoItem
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid CotacaoId { get; set; } = Guid.CreateVersion7();
        public Guid ProdutoId { get; set; } = Guid.CreateVersion7();
        public Guid UnidadeMedidaId { get; set; } = Guid.CreateVersion7();
        public decimal Quantidade { get; set; }
        public Guid? CotacaoItemFornecedorEscolhidoId { get; set; } = Guid.CreateVersion7();
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