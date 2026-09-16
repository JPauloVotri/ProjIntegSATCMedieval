namespace MedievalApi.Models
{
    public class Estoque
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public Guid ProdutoId { get; set; } = Guid.CreateVersion7();
        public decimal QuantidadeDisponivel { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime? UltimaEntrada { get; set; }
        public DateTime? UltimaSaida { get; set; }
        public DateTime AtualizadoEm { get; set; }

        // Navegação
        public Produto? Produto { get; set; }
    }
}
