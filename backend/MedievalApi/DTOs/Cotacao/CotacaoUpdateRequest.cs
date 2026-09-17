using System.ComponentModel.DataAnnotations;
using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Cotacao;

public record CotacaoUpdateRequest(
    [Required(ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    [Required(ErrorMessage = "Data de solicitação é obrigatória.")]
    DateTime DataSolicitacao,

    DateTime? DataLimiteResposta,

    [Required(ErrorMessage = "Status da cotação é obrigatório.")]
    StatusCotacao Status,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
