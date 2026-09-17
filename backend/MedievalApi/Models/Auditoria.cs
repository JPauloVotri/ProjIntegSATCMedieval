using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class Auditoria
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid? UsuarioId { get; set; }
    public Guid? RegistroId { get; set; }
    public string Tabela { get; set; } = string.Empty;
    public TipoAuditoria Tipo { get; set; }
    public string DadosAnteriores { get; set; } = string.Empty;
    public string DadosNovos { get; set; } = string.Empty;
    public string? Ip { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    // Navegação
    public Usuario? Usuario { get; set; }
}
