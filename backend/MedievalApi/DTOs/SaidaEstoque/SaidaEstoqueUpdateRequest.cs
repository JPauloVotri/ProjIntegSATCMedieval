using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.SaidaEstoque;

public record SaidaEstoqueUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    DateTime? DataSaida,

    [EnumDataType(typeof(TipoSaidaEstoque), ErrorMessage = "Tipo inválido.")]
    TipoSaidaEstoque Tipo,

    [EnumDataType(typeof(StatusSaidaEstoque), ErrorMessage = "Status inválido.")]
    StatusSaidaEstoque Status,

    [MaxLength(255, ErrorMessage = "Motivo deve ter no máximo 255 caracteres.")]
    string? Motivo,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
