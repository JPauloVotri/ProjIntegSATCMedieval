namespace MedievalApi.Models;

public class Categoria
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<Produto> Produtos { get; set; } = [];
}
