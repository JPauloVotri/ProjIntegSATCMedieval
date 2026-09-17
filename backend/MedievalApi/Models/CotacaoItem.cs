using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class CotacaoItem
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid CotacaoId { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid UnidadeMedidaId { get; set; }
    public decimal Quantidade { get; set; }
    public Guid? CotacaoItemFornecedorEscolhidoId { get; set; }
    public StatusCotacaoItem Status { get; set; } = StatusCotacaoItem.Pendente;
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    // Navegações
    public Cotacao? Cotacao { get; set; }
    public Produto? Produto { get; set; }
    public UnidadeMedida? UnidadeMedida { get; set; }
    public CotacaoItemFornecedor? CotacaoItemFornecedorEscolhido { get; set; }
}
