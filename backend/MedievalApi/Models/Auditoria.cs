using MedievalApi.Models.Enums;
using System.Text.Json;

namespace MedievalApi.Models
{
    public class Auditoria
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();   
        public Guid? UsuarioId { get; set; } = Guid.CreateVersion7();
        public Guid? RegistroId { get; set; } = Guid.CreateVersion7();
        public string Tabela { get; set; } = string.Empty;
        public TipoAuditoria Tipo { get; set; }
        public JsonDocument? DadosAnteriores { get; set; }
        public JsonDocument? DadosNovos { get; set; }
        public string? Ip { get; set; }
        public string? UserAgent { get; set; }
        public DateTime CriadoEm { get; set; }

        // Navegação
        public Usuario? Usuario { get; set; }
    }
}
