using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class PedidoCompra
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid FornecedorId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid? CotacaoId { get; set; }
    public DateTime DataPedido { get; set; }
    public DateTime? DataPrevistaEntrega { get; set; }
    public DateTime? DataRecebimento { get; set; }
    public StatusPedidoCompra Status { get; set; }
        = StatusPedidoCompra.Rascunho;
    public decimal Total { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    // Navegações
    public Fornecedor? Fornecedor { get; set; }
    public Usuario? Usuario { get; set; }
    public Cotacao? Cotacao { get; set; }
    public ICollection<PedidoCompraItem> Itens { get; set; } = [];
}
