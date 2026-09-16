namespace MedievalApi.Models;

public class Fornecedor
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string RazaoSocial { get; set; } = string.Empty;
    public string? NomeFantasia { get; set; }
    public string? Documento { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? NomeContato { get; set; }
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? DiasEntrega { get; set; }
    public string? CondicaoPagamento { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}
