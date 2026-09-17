using MedievalApi.Models.Enums;
using System.Text.Json;

namespace MedievalApi.Models
{
    public class Auditoria
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? RegistroId { get; set; }
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
