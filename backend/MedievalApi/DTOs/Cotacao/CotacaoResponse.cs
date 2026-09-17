using MedievalApi.Models.Enums;

namespace MedievalApi.DTOs.Cotacao;

public record CotacaoResponse(
    int Id,
    int UsuarioId,
    DateTime DataSolicitacao,
    DateTime? DataLimiteResposta,
    StatusCotacao Status,
    string? Observacao,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);
