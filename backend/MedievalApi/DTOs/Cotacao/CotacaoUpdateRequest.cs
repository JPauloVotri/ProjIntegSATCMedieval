using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Cotacao;

public record CotacaoUpdateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    [Required(ErrorMessage = "Data de solicitação é obrigatória.")]
    DateTime DataSolicitacao,

    DateTime? DataLimiteResposta,

    [EnumDataType(typeof(StatusCotacao), ErrorMessage = "Status inválido.")]
    StatusCotacao Status,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
