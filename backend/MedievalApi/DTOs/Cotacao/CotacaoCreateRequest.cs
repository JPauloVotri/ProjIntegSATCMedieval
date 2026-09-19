using MedievalApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedievalApi.DTOs.Cotacao;

public record CotacaoCreateRequest(
    [Range(1, int.MaxValue, ErrorMessage = "Usuário é obrigatório.")]
    int UsuarioId,

    [Required(ErrorMessage = "Data de solicitação é obrigatória.")]
    DateTime DataSolicitacao,

    DateTime? DataLimiteResposta,
    
    [Required(ErrorMessage = "Status é obrigatório.")]
    StatusCotacao Status,

    [MaxLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres.")]
    string? Observacao
);
