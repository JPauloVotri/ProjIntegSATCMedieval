using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class Auditoria
{
    public int Id { get; set; }
    public int? UsuarioId { get; set; }
    public int? RegistroId { get; set; }
    public string Tabela { get; set; } = string.Empty;
    public TipoAuditoria Tipo { get; set; }
    public string? DadosAnteriores { get; set; }
    public string? DadosNovos { get; set; }
    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Navegação
    public Usuario? Usuario { get; set; }
}
