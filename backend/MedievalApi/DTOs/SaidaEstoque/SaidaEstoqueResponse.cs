using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.SaidaEstoque;

public record SaidaEstoqueResponse(
    int Id,
    int UsuarioId,
    string UsuarioNome,
    DateTime DataSaida,
    TipoSaidaEstoque Tipo,
    StatusSaidaEstoque Status,
    string? Motivo,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
