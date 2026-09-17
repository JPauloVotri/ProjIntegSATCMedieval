using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class SaidaEstoque
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid UsuarioId { get; set; }
    public DateTime DataSaida { get; set; }
    public TipoSaidaEstoque Tipo { get; set; }
    public StatusSaidaEstoque Status { get; set; }
        = StatusSaidaEstoque.Rascunho;
    public string? Motivo { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    // Navegações
    public Usuario? Usuario { get; set; }
    public ICollection<SaidaEstoqueItem> Itens { get; set; } = [];
}
