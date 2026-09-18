using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class Cotacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataSolicitacao { get; set; }
    public DateTime? DataLimiteResposta { get; set; }
    public StatusCotacao Status { get; set; } = StatusCotacao.Rascunho;
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    // Navegação
    public Usuario? Usuario { get; set; }
    public ICollection<CotacaoItem> Itens { get; set; } = [];
}
