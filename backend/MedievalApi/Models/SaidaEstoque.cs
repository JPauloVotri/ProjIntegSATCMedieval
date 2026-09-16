using MedievalApi.Models.Enums;

namespace MedievalApi.Models
{
    public class SaidaEstoque
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid UsuarioId { get; set; } = Guid.CreateVersion7();
        public DateTime DataSaida { get; set; }
        public TipoSaidaEstoque Tipo { get; set; }
        public StatusSaidaEstoque Status { get; set; }
            = StatusSaidaEstoque.Rascunho;
        public string? Motivo { get; set; }
        public string? Observacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegações
        public Usuario? Usuario { get; set; }
        public ICollection<SaidaEstoqueItem> Itens { get; set; }
            = new List<SaidaEstoqueItem>();
    }
}
