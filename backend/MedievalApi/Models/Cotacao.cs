using MedievalApi.Models.Enums;

namespace MedievalApi.Models
{
    public class Cotacao
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid UsuarioId { get; set; } = Guid.CreateVersion7();
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataLimiteResposta { get; set; }
        public StatusCotacao Status { get; set; } = StatusCotacao.Rascunho;
        public string? Observacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegação
        public Usuario? Usuario { get; set; }
    }
}
