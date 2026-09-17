using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class MovimentacaoEstoque
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid EstoqueId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? ReferenciaId { get; set; } //PedidoCompraId, SaidaEstoqueId, etc.
    public string? ReferenciaTipo { get; set; } // PedidoCompra, SaidaEstoque, etc.
    public TipoMovimentacaoEstoque Tipo { get; set; }
    public OrigemMovimentacao Origem { get; set; }
    public decimal Quantidade { get; set; }
    public decimal QuantidadeAnterior { get; set; }
    public decimal QuantidadePosterior { get; set; }
    public decimal? CustoUnitario { get; set; }
    public decimal? ValorTotal { get; set; }
    public string? Lote { get; set; }
    public DateTime? DataValidade { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Navegações
    public Estoque? Estoque { get; set; }
    public Usuario? Usuario { get; set; }
}
