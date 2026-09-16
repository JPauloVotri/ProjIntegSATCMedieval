using MedievalApi.Models.Enums;

namespace MedievalApi.Models;

public class Usuario
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public string Nome
    {
        get;
        set => field = value.Trim();
    } = string.Empty;

    public string Email
    {
        get;
        set => field = NormalizeEmail(value);
    } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;
    public GrupoUsuario GrupoUsuario { get; set; } = GrupoUsuario.Consulta;
    public UsuarioStatus Status { get; set; } = UsuarioStatus.Ativo;
    public DateTime? UltimoLogin { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    private Usuario() { }

    public Usuario(string nome, string email, string senha, GrupoUsuario grupoUsuario)
    {
        Nome = nome.Trim();
        Email = NormalizeEmail(email);
        SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        GrupoUsuario = grupoUsuario;
    }

    public static string NormalizeEmail(string email) =>
        email.Trim().ToLowerInvariant();
}
