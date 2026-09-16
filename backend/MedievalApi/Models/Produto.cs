namespace MedievalApi.Models;

public class Produto
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid CategoriaId { get; set; }
    public GrupoProduto Categoria { get; set; } = null!;
    public Guid UnidadeMedidaId { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; } = null!;
    public string? Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public decimal? EstoqueMaximo { get; set; }
    public decimal CustoMedioEstoque { get; set; }
    public decimal UltimoCusto { get; set; }
    public int? ValidadeDias { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
