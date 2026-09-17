namespace MedievalApi.Models;

public class Estoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public decimal QuantidadeDisponivel { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime? UltimaEntrada { get; set; }
    public DateTime? UltimaSaida { get; set; }
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    // Navegação
    public Produto? Produto { get; set; }
}
